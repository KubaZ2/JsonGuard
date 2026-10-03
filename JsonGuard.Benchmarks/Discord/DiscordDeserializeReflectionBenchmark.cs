using System.Text.Json;
using System.Text.Json.Serialization;
using BenchmarkDotNet.Attributes;

namespace JsonGuard.Benchmarks.Discord;

[MemoryDiagnoser]
public class DiscordDeserializeReflectionBenchmark
{
    private static readonly JsonSerializerOptions s_options = new() { NumberHandling = JsonNumberHandling.AllowReadingFromString };
    private static readonly JsonSerializerOptions s_nullableOptions = new() { RespectNullableAnnotations = true, NumberHandling = JsonNumberHandling.AllowReadingFromString };

    [Benchmark(Baseline = true)]
    [ArgumentsSource(nameof(GetData))]
    public NoChecks.JsonGuild DiscordReflection(ReadOnlySpan<byte> json)
    {
        return JsonSerializer.Deserialize<NoChecks.JsonGuild>(json, s_options)!;
    }

    [Benchmark]
    [ArgumentsSource(nameof(GetData))]
    public JsonGuard.JsonGuild DiscordReflectionWithJsonGuard(ReadOnlySpan<byte> json)
    {
        return JsonSerializer.Deserialize<JsonGuard.JsonGuild>(json, s_options)!;
    }

    [Benchmark]
    [ArgumentsSource(nameof(GetData))]
    public JsonRequired.JsonGuild DiscordReflectionWithJsonRequired(ReadOnlySpan<byte> json)
    {
        return JsonSerializer.Deserialize<JsonRequired.JsonGuild>(json, s_nullableOptions)!;
    }

    [Benchmark]
    [ArgumentsSource(nameof(GetData))]
    public Required.JsonGuild DiscordReflectionWithRequired(ReadOnlySpan<byte> json)
    {
        return JsonSerializer.Deserialize<Required.JsonGuild>(json, s_nullableOptions)!;
    }

    public static IEnumerable<byte[]> GetData()
    {
        yield return JsonData.Guild;
    }
}
