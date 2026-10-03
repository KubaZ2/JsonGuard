using Microsoft.CodeAnalysis;

namespace JsonGuard;

public static class JsonGuardCommon
{
    public static bool IsPropertyGuarded(IPropertySymbol propertySymbol)
    {
        return !propertySymbol.IsImplicitlyDeclared
            && !propertySymbol.IsStatic
            && !propertySymbol.IsIndexer
            && propertySymbol.GetMethod is not null
            && propertySymbol.Type.IsReferenceType
            && propertySymbol.NullableAnnotation is not NullableAnnotation.Annotated
            && !propertySymbol.GetAttributes().Any(a =>
            {
                return a.AttributeClass is { } attributeClass
                    && attributeClass.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)
                        is "global::System.Text.Json.Serialization.JsonIgnoreAttribute"
                        or "global::System.Runtime.CompilerServices.CompilerGeneratedAttribute";
            });
    }
}
