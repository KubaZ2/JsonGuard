using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace JsonGuard;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class JsonGuardDiagnosticSuppressor : DiagnosticSuppressor
{
    private static readonly SuppressionDescriptor s_suppressMustBeNonNullWhenExitingConstructor = new(
        id: "JSONGUARDSRP0001",
        suppressedDiagnosticId: "CS8618",
        justification: "Suppressing CS8618 for non-nullable reference type members as they are validated to be non-null when deserialized by System.Text.Json.");

    private static readonly SuppressionDescriptor s_suppressMustBeNonNullWhenExitingConstructorWithField = new(
        id: "JSONGUARDSRP0002",
        suppressedDiagnosticId: "CS9264",
        justification: "Suppressing CS9264 for non-nullable reference type members as they are validated to be non-null when deserialized by System.Text.Json.");

    public override ImmutableArray<SuppressionDescriptor> SupportedSuppressions => [s_suppressMustBeNonNullWhenExitingConstructor, s_suppressMustBeNonNullWhenExitingConstructorWithField];

    public override void ReportSuppressions(SuppressionAnalysisContext context)
    {
        foreach (var diagnostic in context.ReportedDiagnostics)
        {
            if (diagnostic.Location.SourceTree is not { } sourceTree || sourceTree.GetRoot(context.CancellationToken).FindNode(diagnostic.Location.SourceSpan) is not { } node)
                continue;

            var model = context.GetSemanticModel(node.SyntaxTree);
            var declaredSymbol = model.GetDeclaredSymbol(node, context.CancellationToken);
            if (declaredSymbol is not IPropertySymbol propertySymbol || !JsonGuardCommon.IsPropertyGuarded(propertySymbol))
                continue;

            if (propertySymbol.ContainingType.GetAttributes().Any(a =>
            {
                return a.AttributeClass is { } attributeClass
                    && attributeClass.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat) is "global::JsonGuard.JsonGuardAttribute";
            }))
            {
                if (diagnostic.Id switch
                {
                    "CS8618" => s_suppressMustBeNonNullWhenExitingConstructor,
                    "CS9264" => s_suppressMustBeNonNullWhenExitingConstructorWithField,
                    _ => null,
                } is not { } descriptor)
                    continue;

                context.ReportSuppression(Suppression.Create(descriptor, diagnostic));
            }
        }
    }
}
