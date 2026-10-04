using System.Text.Json;
using System.Text.Json.Serialization;
using BenchmarkDotNet.Attributes;

namespace JsonGuard.Benchmarks.Complex;

[JsonSourceGenerationOptions(NumberHandling = JsonNumberHandling.AllowReadingFromString)]
[JsonSerializable(typeof(NoChecks.JsonGuild))]
public partial class ComplexNoChecksSerializerContext : JsonSerializerContext;

[JsonSourceGenerationOptions(NumberHandling = JsonNumberHandling.AllowReadingFromString)]
[JsonSerializable(typeof(JsonGuard.JsonGuild))]
public partial class ComplexJsonGuardSerializerContext : JsonSerializerContext;

[JsonSourceGenerationOptions(RespectNullableAnnotations = true, NumberHandling = JsonNumberHandling.AllowReadingFromString)]
[JsonSerializable(typeof(JsonRequired.JsonGuild))]
public partial class ComplexJsonRequiredSerializerContext : JsonSerializerContext;

[JsonSourceGenerationOptions(RespectNullableAnnotations = true, NumberHandling = JsonNumberHandling.AllowReadingFromString)]
[JsonSerializable(typeof(Required.JsonGuild))]
public partial class ComplexRequiredSerializerContext : JsonSerializerContext;

[MemoryDiagnoser]
public class ComplexDeserializeSourceGenBenchmark
{
    [Benchmark(Baseline = true)]
    [ArgumentsSource(nameof(GetData))]
    public NoChecks.JsonGuild ComplexSourceGen(ReadOnlySpan<byte> json)
    {
        return JsonSerializer.Deserialize(json, ComplexNoChecksSerializerContext.Default.JsonGuild)!;
    }

    [Benchmark]
    [ArgumentsSource(nameof(GetData))]
    public JsonGuard.JsonGuild ComplexSourceGenWithJsonGuard(ReadOnlySpan<byte> json)
    {
        return JsonSerializer.Deserialize(json, ComplexJsonGuardSerializerContext.Default.JsonGuild)!;
    }

    [Benchmark]
    [ArgumentsSource(nameof(GetData))]
    public JsonRequired.JsonGuild ComplexSourceGenWithJsonRequired(ReadOnlySpan<byte> json)
    {
        return JsonSerializer.Deserialize(json, ComplexJsonRequiredSerializerContext.Default.JsonGuild)!;
    }

    [Benchmark]
    [ArgumentsSource(nameof(GetData))]
    public Required.JsonGuild ComplexSourceGenWithRequired(ReadOnlySpan<byte> json)
    {
        return JsonSerializer.Deserialize(json, ComplexRequiredSerializerContext.Default.JsonGuild)!;
    }

    public static IEnumerable<byte[]> GetData()
    {
        yield return JsonData.Guild;
    }
}
