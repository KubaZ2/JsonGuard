using System.Text.Json;
using System.Text.Json.Serialization;
using BenchmarkDotNet.Attributes;

namespace JsonGuard.Benchmarks.Simple;

[JsonSerializable(typeof(PayloadNoChecks))]
public partial class SimpleNoChecksSerializerContext : JsonSerializerContext;

[JsonSerializable(typeof(PayloadJsonGuard))]
public partial class SimpleJsonGuardSerializerContext : JsonSerializerContext;

[JsonSourceGenerationOptions(RespectNullableAnnotations = true)]
[JsonSerializable(typeof(PayloadJsonRequired))]
public partial class SimpleJsonRequiredSerializerContext : JsonSerializerContext;

[JsonSourceGenerationOptions(RespectNullableAnnotations = true)]
[JsonSerializable(typeof(PayloadRequired))]
public partial class SimpleRequiredSerializerContext : JsonSerializerContext;

[MemoryDiagnoser]
public class SimpleDeserializeSourceGenBenchmark
{
    [Benchmark(Baseline = true)]
    [ArgumentsSource(nameof(GetData))]
    public PayloadNoChecks SourceGen(ReadOnlySpan<byte> json)
    {
        return JsonSerializer.Deserialize(json, SimpleNoChecksSerializerContext.Default.PayloadNoChecks)!;
    }

    [Benchmark]
    [ArgumentsSource(nameof(GetData))]
    public PayloadJsonGuard SourceGenWithJsonGuard(ReadOnlySpan<byte> json)
    {
        return JsonSerializer.Deserialize(json, SimpleJsonGuardSerializerContext.Default.PayloadJsonGuard)!;
    }

    [Benchmark]
    [ArgumentsSource(nameof(GetData))]
    public PayloadJsonRequired SourceGenWithJsonRequired(ReadOnlySpan<byte> json)
    {
        return JsonSerializer.Deserialize(json, SimpleJsonRequiredSerializerContext.Default.PayloadJsonRequired)!;
    }

    [Benchmark]
    [ArgumentsSource(nameof(GetData))]
    public PayloadRequired SourceGenWithRequired(ReadOnlySpan<byte> json)
    {
        return JsonSerializer.Deserialize(json, SimpleRequiredSerializerContext.Default.PayloadRequired)!;
    }

    public static IEnumerable<byte[]> GetData()
    {
        yield return JsonData.Json;
    }
}
