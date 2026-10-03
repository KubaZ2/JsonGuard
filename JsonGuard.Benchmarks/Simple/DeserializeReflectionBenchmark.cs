using System.Text.Json;
using BenchmarkDotNet.Attributes;

namespace JsonGuard.Benchmarks.Simple;

[MemoryDiagnoser]
public class DeserializeReflectionBenchmark
{
    private static readonly JsonSerializerOptions s_defaultOptions = new();
    private static readonly JsonSerializerOptions s_nullableOptions = new() { RespectNullableAnnotations = true };

    [Benchmark(Baseline = true)]
    [ArgumentsSource(nameof(GetData))]
    public PayloadNoChecks Reflection(ReadOnlySpan<byte> json)
    {
        return JsonSerializer.Deserialize<PayloadNoChecks>(json, s_defaultOptions)!;
    }

    [Benchmark]
    [ArgumentsSource(nameof(GetData))]
    public PayloadJsonGuard ReflectionWithJsonGuard(ReadOnlySpan<byte> json)
    {
        return JsonSerializer.Deserialize<PayloadJsonGuard>(json, s_defaultOptions)!;
    }

    [Benchmark]
    [ArgumentsSource(nameof(GetData))]
    public PayloadJsonRequired ReflectionWithJsonRequired(ReadOnlySpan<byte> json)
    {
        return JsonSerializer.Deserialize<PayloadJsonRequired>(json, s_nullableOptions)!;
    }

    [Benchmark]
    [ArgumentsSource(nameof(GetData))]
    public PayloadRequired ReflectionWithRequired(ReadOnlySpan<byte> json)
    {
        return JsonSerializer.Deserialize<PayloadRequired>(json, s_nullableOptions)!;
    }

    public static IEnumerable<byte[]> GetData()
    {
        yield return JsonData.Json;
    }
}
