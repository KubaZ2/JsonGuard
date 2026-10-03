using System.Text.Json;
using System.Text.Json.Serialization;
using BenchmarkDotNet.Attributes;

namespace JsonGuard.Benchmarks.Discord;

[JsonSourceGenerationOptions(NumberHandling = JsonNumberHandling.AllowReadingFromString)]
[JsonSerializable(typeof(NoChecks.JsonGuild))]
public partial class DiscordNoChecksSerializerContext : JsonSerializerContext;

[JsonSourceGenerationOptions(NumberHandling = JsonNumberHandling.AllowReadingFromString)]
[JsonSerializable(typeof(JsonGuard.JsonGuild))]
public partial class DiscordJsonGuardSerializerContext : JsonSerializerContext;

[JsonSourceGenerationOptions(RespectNullableAnnotations = true, NumberHandling = JsonNumberHandling.AllowReadingFromString)]
[JsonSerializable(typeof(JsonRequired.JsonGuild))]
public partial class DiscordJsonRequiredSerializerContext : JsonSerializerContext;

[JsonSourceGenerationOptions(RespectNullableAnnotations = true, NumberHandling = JsonNumberHandling.AllowReadingFromString)]
[JsonSerializable(typeof(Required.JsonGuild))]
public partial class DiscordRequiredSerializerContext : JsonSerializerContext;

[MemoryDiagnoser]
public class DiscordDeserializeSourceGenBenchmark
{
    [Benchmark(Baseline = true)]
    [ArgumentsSource(nameof(GetData))]
    public NoChecks.JsonGuild DiscordSourceGen(ReadOnlySpan<byte> json)
    {
        return JsonSerializer.Deserialize(json, DiscordNoChecksSerializerContext.Default.JsonGuild)!;
    }

    [Benchmark]
    [ArgumentsSource(nameof(GetData))]
    public JsonGuard.JsonGuild DiscordSourceGenWithJsonGuard(ReadOnlySpan<byte> json)
    {
        return JsonSerializer.Deserialize(json, DiscordJsonGuardSerializerContext.Default.JsonGuild)!;
    }

    [Benchmark]
    [ArgumentsSource(nameof(GetData))]
    public JsonRequired.JsonGuild DiscordSourceGenWithJsonRequired(ReadOnlySpan<byte> json)
    {
        return JsonSerializer.Deserialize(json, DiscordJsonRequiredSerializerContext.Default.JsonGuild)!;
    }

    [Benchmark]
    [ArgumentsSource(nameof(GetData))]
    public Required.JsonGuild DiscordSourceGenWithRequired(ReadOnlySpan<byte> json)
    {
        return JsonSerializer.Deserialize(json, DiscordRequiredSerializerContext.Default.JsonGuild)!;
    }

    public static IEnumerable<byte[]> GetData()
    {
        yield return JsonData.Guild;
    }
}
