using System.Text.Json;
using System.Text.Json.Serialization;
using BenchmarkDotNet.Attributes;

namespace JsonGuard.Benchmarks.Complex;

[MemoryDiagnoser]
public class ComplexDeserializeReflectionBenchmark
{
    private static readonly JsonSerializerOptions s_options = new() { NumberHandling = JsonNumberHandling.AllowReadingFromString };
    private static readonly JsonSerializerOptions s_nullableOptions = new() { RespectNullableAnnotations = true, NumberHandling = JsonNumberHandling.AllowReadingFromString };

    [Benchmark(Baseline = true)]
    [ArgumentsSource(nameof(GetData))]
    public NoChecks.JsonGuild ComplexReflection(ReadOnlySpan<byte> json)
    {
        return JsonSerializer.Deserialize<NoChecks.JsonGuild>(json, s_options)!;
    }

    [Benchmark]
    [ArgumentsSource(nameof(GetData))]
    public JsonGuard.JsonGuild ComplexReflectionWithJsonGuard(ReadOnlySpan<byte> json)
    {
        return JsonSerializer.Deserialize<JsonGuard.JsonGuild>(json, s_options)!;
    }

    [Benchmark]
    [ArgumentsSource(nameof(GetData))]
    public JsonRequired.JsonGuild ComplexReflectionWithJsonRequired(ReadOnlySpan<byte> json)
    {
        return JsonSerializer.Deserialize<JsonRequired.JsonGuild>(json, s_nullableOptions)!;
    }

    [Benchmark]
    [ArgumentsSource(nameof(GetData))]
    public Required.JsonGuild ComplexReflectionWithRequired(ReadOnlySpan<byte> json)
    {
        return JsonSerializer.Deserialize<Required.JsonGuild>(json, s_nullableOptions)!;
    }

    public static IEnumerable<byte[]> GetData()
    {
        yield return JsonData.Guild;
    }
}
