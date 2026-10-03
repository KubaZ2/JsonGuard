using System.CodeDom.Compiler;
using System.Collections.Immutable;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace JsonGuard;

[Generator(LanguageNames.CSharp)]
public sealed class JsonGuardGenerator : IIncrementalGenerator
{
    private static readonly SymbolDisplayFormat s_genericNameFormat = new(
        typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameOnly,
        genericsOptions: SymbolDisplayGenericsOptions.IncludeTypeParameters);

    private sealed record TypeInfo(string MetadataName, string GenericName, string Type)
    {
        public static Result<TypeInfo> Create(INamedTypeSymbol namedTypeSymbol)
        {
            if (GetType(namedTypeSymbol) is not { } type)
                return new Result<TypeInfo>.Error(Diagnostic.Create(
                    s_unsupportedTypeKind,
                    namedTypeSymbol.Locations.FirstOrDefault()));

            return new Result<TypeInfo>.Success(new(namedTypeSymbol.MetadataName,
                                                    namedTypeSymbol.ToDisplayString(s_genericNameFormat),
                                                    type));
        }

        private static string? GetType(INamedTypeSymbol namedTypeSymbol)
        {
            return namedTypeSymbol switch
            {
                { TypeKind: TypeKind.Class, IsRecord: false } => "class",
                { TypeKind: TypeKind.Class, IsRecord: true } => "record class",
                { TypeKind: TypeKind.Struct, IsRecord: false } => "struct",
                { TypeKind: TypeKind.Struct, IsRecord: true } => "record struct",
                { TypeKind: TypeKind.Interface } => "interface",
                _ => null,
            };
        }

    }

    private abstract record Result<T>
    {
        public sealed record Success(T Value) : Result<T>;

        public sealed record Error(Diagnostic Diagnostic) : Result<T>;
    }

    private sealed record GuardTypeInfo(TypeInfo Type, string? Namespace, ValueImmutableArray<GuardMemberInfo> Members, ValueImmutableArray<TypeInfo> ContainingTypes);

    private sealed class ValueImmutableArray<T>(ImmutableArray<T> array) : IEquatable<ValueImmutableArray<T>?>
    {
        public ImmutableArray<T> Array => array;

        public static bool operator ==(ValueImmutableArray<T>? left, ValueImmutableArray<T>? right)
        {
            return left is null
                ? right is null
                : left.Equals(right);
        }

        public static bool operator !=(ValueImmutableArray<T>? left, ValueImmutableArray<T>? right) => !(left == right);

        public override bool Equals(object? obj)
        {
            if (obj is not ValueImmutableArray<T> other)
                return false;

            return array.SequenceEqual(other.Array);
        }

        public bool Equals(ValueImmutableArray<T>? other)
        {
            if (other is null)
                return false;

            return array.SequenceEqual(other.Array);
        }

        public override int GetHashCode()
        {
            HashCode hashCode = new();

            foreach (var member in array)
                hashCode.Add(member);

            return hashCode.ToHashCode();
        }
    }

    private sealed record GuardMemberInfo(string MemberName, string ParentTypeFullName);

    private static readonly DiagnosticDescriptor s_invalidTargetSymbol = new(
        "JG0001",
        "Invalid target symbol",
        "The target symbol is not a named type symbol",
        "Usage",
        DiagnosticSeverity.Warning,
        true);

    private static readonly DiagnosticDescriptor s_missingPartialModifier = new(
        "JG0002",
        "Missing partial modifier",
        "The target type and all containing types must be declared as partial",
        "Usage",
        DiagnosticSeverity.Warning,
        true);

    private static readonly DiagnosticDescriptor s_noNonNullableReferenceTypeProperties = new(
        "JG0003",
        "No non-nullable reference type properties",
        "The target type should have at least one non-nullable reference type property with a getter supposed. Consider removing the JsonGuard.JsonGuardAttribute if this is intentional.",
        "Usage",
        DiagnosticSeverity.Warning,
        true);

    private static readonly DiagnosticDescriptor s_unsupportedTypeKind = new(
        "JG0004",
        "Unsupported type kind",
        "The target type should be a class, struct, record class, or record struct",
        "Usage",
        DiagnosticSeverity.Warning,
        true);

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(context =>
        {
            context.AddEmbeddedAttributeDefinition();

            context.AddSource("JsonGuardAttribute.g.cs",
                """
                namespace JsonGuard;

                [global::Microsoft.CodeAnalysis.EmbeddedAttribute]
                [global::System.AttributeUsageAttribute(global::System.AttributeTargets.Class | global::System.AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
                public sealed class JsonGuardAttribute : global::System.Attribute;
                """);
        });

        var results = context.SyntaxProvider.ForAttributeWithMetadataName("JsonGuard.JsonGuardAttribute",
                                                                          (syntaxNode, cancellationToken) => syntaxNode is TypeDeclarationSyntax,
                                                                          Transform).Collect();

        context.RegisterSourceOutput(results, RegisterSourceOutput);
    }

    private static Result<(GuardTypeInfo, Diagnostic?)> Transform(GeneratorAttributeSyntaxContext context, CancellationToken cancellationToken)
    {
        if (context.TargetSymbol is not INamedTypeSymbol namedTypeSymbol)
        {
            return new Result<(GuardTypeInfo, Diagnostic?)>.Error(Diagnostic.Create(
                s_invalidTargetSymbol,
                context.TargetNode.GetLocation()));
        }

        var targetNode = (TypeDeclarationSyntax)context.TargetNode;

        if (!targetNode.Modifiers.Any(SyntaxKind.PartialKeyword))
            return new Result<(GuardTypeInfo, Diagnostic?)>.Error(Diagnostic.Create(
                s_missingPartialModifier,
                context.TargetNode.GetLocation()));

        var members = GetGuardMembers(namedTypeSymbol);

        var diagnostic = members.IsEmpty
            ? Diagnostic.Create(
                s_noNonNullableReferenceTypeProperties,
                context.TargetNode.GetLocation())
            : null;

        var containingTypesResult = GetContainingTypes(namedTypeSymbol);

        if (containingTypesResult is Result<ImmutableArray<TypeInfo>>.Error error)
            return new Result<(GuardTypeInfo, Diagnostic?)>.Error(error.Diagnostic);

        var containingTypes = ((Result<ImmutableArray<TypeInfo>>.Success)containingTypesResult).Value;

        var typeInfoResult = TypeInfo.Create(namedTypeSymbol);

        if (typeInfoResult is Result<TypeInfo>.Error typeInfoError)
            return new Result<(GuardTypeInfo, Diagnostic?)>.Error(typeInfoError.Diagnostic);

        var typeInfo = ((Result<TypeInfo>.Success)typeInfoResult).Value;

        var containingNamespace = namedTypeSymbol.ContainingNamespace;

        var namespaceString = containingNamespace.IsGlobalNamespace
            ? null
            : containingNamespace.ToDisplayString();

        GuardTypeInfo guardTypeInfo = new(typeInfo,
                                          namespaceString,
                                          new(members),
                                          new(containingTypes));

        return new Result<(GuardTypeInfo, Diagnostic?)>.Success((guardTypeInfo, diagnostic));
    }

    private static void RegisterSourceOutput(SourceProductionContext context, ImmutableArray<Result<(GuardTypeInfo, Diagnostic?)>> results)
    {
        using IndentedTextWriter writer = new(new StringWriter());

        writer.WriteLine("// <auto-generated />");
        writer.WriteLine("#nullable enable");
        writer.WriteLine();

        WriteThrowHelper(writer);

        foreach (var result in results)
        {
            writer.WriteLine();

            if (result is Result<(GuardTypeInfo, Diagnostic?)>.Error { Diagnostic: var diagnostic })
            {
                context.ReportDiagnostic(diagnostic);
                continue;
            }

            var (info, diag) = ((Result<(GuardTypeInfo, Diagnostic?)>.Success)result).Value;

            if (diag is not null)
                context.ReportDiagnostic(diag);

            Generate(writer, info);
        }

        var source = SourceText.From(writer.InnerWriter.ToString(), Encoding.UTF8);

        context.AddSource("JsonGuard.g.cs", source);
    }

    private static void WriteThrowHelper(IndentedTextWriter writer)
    {
        writer.WriteLine("static file class ThrowHelper");
        writer.WriteLine("{");
        writer.Indent++;

        writer.WriteLine("[global::System.Diagnostics.CodeAnalysis.DoesNotReturnAttribute]");
        writer.WriteLine("[global::System.Diagnostics.StackTraceHiddenAttribute]");
        writer.WriteLine("public static void Throw(string memberName)");
        writer.WriteLine("{");
        writer.Indent++;

        writer.WriteLine("throw new global::System.Text.Json.JsonException($\"The member '{memberName}' cannot be null.\");");

        writer.Indent--;
        writer.WriteLine("}");

        writer.Indent--;
        writer.WriteLine("}");
    }

    private static ImmutableArray<GuardMemberInfo> GetGuardMembers(INamedTypeSymbol namedTypeSymbol)
    {
        var result = ImmutableArray.CreateBuilder<GuardMemberInfo>();

        var currentSymbol = namedTypeSymbol;

        HashSet<IPropertySymbol> handledProperties = new(SymbolEqualityComparer.Default);

        do
        {
            var members = currentSymbol.GetMembers();
            if (members.IsEmpty)
                continue;

            var currentSymbolFullName = currentSymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);

            foreach (var member in members)
            {
                if (member is not IPropertySymbol propertySymbol || !JsonGuardCommon.IsPropertyGuarded(propertySymbol))
                    continue;

                if (!UpdateHandledProperties(propertySymbol, handledProperties))
                    continue;

                result.Add(new(propertySymbol.Name, currentSymbolFullName));
            }
        }
        while ((currentSymbol = currentSymbol!.BaseType) is not null);

        return result.ToImmutable();

        static bool UpdateHandledProperties(IPropertySymbol propertySymbol, HashSet<IPropertySymbol> handledProperties)
        {
            var currentProperty = propertySymbol;

            while (currentProperty.OverriddenProperty is { } overriddenProperty)
                currentProperty = overriddenProperty;

            return handledProperties.Add(currentProperty.OriginalDefinition);
        }
    }

    private static Result<ImmutableArray<TypeInfo>> GetContainingTypes(INamedTypeSymbol namedTypeSymbol)
    {
        var result = ImmutableArray.CreateBuilder<TypeInfo>();

        var currentSymbol = namedTypeSymbol;

        while ((currentSymbol = currentSymbol!.ContainingType) is not null)
        {
            // This check is best effort
            if (currentSymbol.DeclaringSyntaxReferences is [var syntaxReference, ..]
                && syntaxReference.GetSyntax() is TypeDeclarationSyntax { Modifiers: var modifiers }
                && !modifiers.Any(SyntaxKind.PartialKeyword))
                return new Result<ImmutableArray<TypeInfo>>.Error(Diagnostic.Create(
                    s_missingPartialModifier,
                    syntaxReference.GetSyntax().GetLocation()));

            var typeInfoResult = TypeInfo.Create(currentSymbol);

            if (typeInfoResult is Result<TypeInfo>.Error error)
                return new Result<ImmutableArray<TypeInfo>>.Error(error.Diagnostic);

            var typeInfo = ((Result<TypeInfo>.Success)typeInfoResult).Value;

            result.Add(typeInfo);
        }

        result.Reverse();

        return new Result<ImmutableArray<TypeInfo>>.Success(result.ToImmutable());
    }

    private static void Generate(IndentedTextWriter writer, GuardTypeInfo info)
    {
        var @namespace = info.Namespace;

        if (@namespace is not null)
        {
            writer.Write("namespace ");
            writer.WriteLine(info.Namespace);

            writer.WriteLine("{");
            writer.Indent++;
        }

        var type = info.Type;

        var containingTypes = info.ContainingTypes.Array;

        foreach (var containingType in containingTypes)
        {
            writer.Write("partial ");
            writer.Write(containingType.Type);
            writer.Write(" ");
            writer.WriteLine(containingType.GenericName);

            writer.WriteLine("{");
            writer.Indent++;
        }

        writer.Write("partial ");
        writer.Write(type.Type);
        writer.Write(" ");
        writer.Write(type.GenericName);

        writer.WriteLine(" : global::System.Text.Json.Serialization.IJsonOnDeserialized");

        writer.WriteLine("{");
        writer.Indent++;

        writer.WriteLine("void global::System.Text.Json.Serialization.IJsonOnDeserialized.OnDeserialized()");
        writer.WriteLine("{");
        writer.Indent++;

        var members = info.Members.Array;

        var memberCount = members.Length;

        for (int i = 0; i < memberCount; i++)
        {
            var member = info.Members.Array[i];

            var memberName = member.MemberName;

            writer.Write("if (((");
            writer.Write(member.ParentTypeFullName);
            writer.Write(")this).");
            writer.Write(memberName);
            writer.WriteLine(" is null)");

            writer.Indent++;

            // Roslyn reports CS8082 for nameof with a cast, so we need to use a string literal instead
            writer.Write("global::ThrowHelper.Throw(\"");
            writer.Write(memberName);
            writer.WriteLine("\");");

            writer.Indent--;

            if (i != memberCount - 1)
                writer.WriteLine();
        }

        writer.Indent--;
        writer.WriteLine("}");

        writer.Indent--;
        writer.WriteLine("}");

        var containingTypeCount = containingTypes.Length;

        for (int i = 0; i < containingTypeCount; i++)
        {
            writer.Indent--;
            writer.WriteLine("}");
        }

        if (@namespace is not null)
        {
            writer.Indent--;
            writer.WriteLine("}");
        }
    }
}
