# JsonGuard

<div align="center">
  <p>
    <a href="https://www.nuget.org/packages/JsonGuard"><img alt="Version" src="https://img.shields.io/nuget/vpre/JsonGuard?logo=nuget&label=NuGet&color=004880" /></a>
    <a href="LICENSE"><img alt="GitHub License" src="https://img.shields.io/github/license/KubaZ2/JsonGuard" /></a>
  </p>
</div>

An efficient JSON validation library for System.Text.Json, based on Source Generation.

## Motivation

By default, System.Text.Json allows you to validate required properties using the `[JsonRequired]` attribute or the `required` keyword. However, these built-in methods introduce significant performance overhead, especially in Source-Generated and Native AOT scenarios.

JsonGuard circumvents this overhead by using a Roslyn Source Generator to implement `IJsonOnDeserialized`. Instead of evaluating requirements property-by-property during the deserialization pipeline, JsonGuard does a single, highly optimized null-check pass at the very end of object construction.

## How it works

When you define `[JsonGuard]` attribute on a class, the source generator will generate an implementation of `IJsonOnDeserialized` for that class. The generated implementation will check if all non-nullable properties are present in the JSON document during deserialization. If any required property is missing, a `JsonException` will be thrown.

### Example

Let's say you have the following class:

```csharp
[JsonGuard]
public partial class User
{
    public string Name { get; set; }

    public string? Nickname { get; set; }

    public int Age { get; set; }

    public string Email { get; set; }
}
```

The generated code will effectively do the following:

```csharp
public partial class User : IJsonOnDeserialized
{
    void IJsonOnDeserialized.OnDeserialized()
    {
        if (Name is null)
            ThrowHelper.Throw(nameof(Name));

        if (Email is null)
            ThrowHelper.Throw(nameof(Email));
    }
}
```

## Installation

Install the package via NuGet:

```bash
dotnet add package JsonGuard
```

## Features

- Supports both JIT and Native AOT compilation.
- Works with both reflection-based and source-generated serialization.
- Skips nullable properties and value-type properties.
- Automatically suppresses nullability warnings for validated properties.
- Supports complex type hierarchies.
- Supports classes, structs, records, and record structs.
- Supports virtual and abstract properties without duplicating the validation logic.

## Benchmarks

The source code of benchmarks can be found in the [JsonGuard.Benchmarks](JsonGuard.Benchmarks) directory.

### Results

The benchmarks evaluate performance across two runtimes (JIT and Native AOT) using two distinct workloads:
- **Complex Scenario**: A large JSON document (a large Discord guild payload).
- **Simple Scenario**: A small JSON document (basic user information).

> [!NOTE]
> - JIT: Benchmarks both reflection-based and source-generated deserialization.
> - Native AOT: Benchmarks only source generation, as Native AOT inherently lacks support for reflection-based deserialization.

#### JIT

<details>
<summary>Host Environment</summary>

```
BenchmarkDotNet v0.15.8, Linux Ubuntu 24.04.4 LTS (Noble Numbat)
12th Gen Intel Core i9-12900K 3.19GHz, 1 CPU, 24 logical and 12 physical cores
.NET SDK 10.0.401
  [Host]    : .NET 10.0.12 (10.0.12, 10.0.1226.42308), X64 RyuJIT x86-64-v3
  .NET 10.0 : .NET 10.0.12 (10.0.12, 10.0.1226.42308), X64 RyuJIT x86-64-v3

Job=.NET 10.0  Runtime=.NET 10.0  Toolchain=net10.0
```

</details>

<details>
<summary>Complex, reflection based</summary>

| Method                             | json          | Mean     | Error     | StdDev    | Ratio | Gen0     | Gen1    | Allocated | Alloc Ratio |
|----------------------------------- |-------------- |---------:|----------:|----------:|------:|---------:|--------:|----------:|------------:|
| ComplexReflection                  | Byte[1872295] | 3.323 ms | 0.0156 ms | 0.0146 ms |  1.00 | 148.4375 | 78.1250 |   2.24 MB |        1.00 |
| **ComplexReflectionWithJsonGuard** | Byte[1872295] | 3.310 ms | 0.0143 ms | 0.0134 ms |  1.00 | 148.4375 | 78.1250 |   2.24 MB |        1.00 |
| ComplexReflectionWithJsonRequired  | Byte[1872295] | 3.632 ms | 0.0214 ms | 0.0200 ms |  1.09 | 175.7813 | 93.7500 |   2.64 MB |        1.18 |
| ComplexReflectionWithRequired      | Byte[1872295] | 3.611 ms | 0.0253 ms | 0.0224 ms |  1.09 | 175.7813 | 93.7500 |   2.64 MB |        1.18 |
</details>

<details>
<summary>Complex, source generated</summary>

| Method                            | json          | Mean     | Error     | StdDev    | Ratio | RatioSD | Gen0     | Gen1     | Allocated | Alloc Ratio |
|---------------------------------- |-------------- |---------:|----------:|----------:|------:|--------:|---------:|---------:|----------:|------------:|
| ComplexSourceGen                  | Byte[1872295] | 3.290 ms | 0.0159 ms | 0.0149 ms |  1.00 |    0.01 | 148.4375 | 125.0000 |   2.24 MB |        1.00 |
| **ComplexSourceGenWithJsonGuard** | Byte[1872295] | 3.242 ms | 0.0159 ms | 0.0149 ms |  0.99 |    0.01 | 148.4375 | 125.0000 |   2.24 MB |        1.00 |
| ComplexSourceGenWithJsonRequired  | Byte[1872295] | 3.504 ms | 0.0180 ms | 0.0160 ms |  1.07 |    0.01 | 175.7813 | 152.3438 |   2.64 MB |        1.18 |
| ComplexSourceGenWithRequired      | Byte[1872295] | 6.489 ms | 0.0569 ms | 0.0532 ms |  1.97 |    0.02 | 218.7500 | 187.5000 |   3.35 MB |        1.50 |
</details>

<details>
<summary>Simple, reflection based</summary>

| Method                            | json      | Mean     | Error     | StdDev    | Ratio | Gen0   | Allocated | Alloc Ratio |
|---------------------------------- |---------- |---------:|----------:|----------:|------:|-------:|----------:|------------:|
| SimpleReflection                  | Byte[909] | 2.068 μs | 0.0155 μs | 0.0145 μs |  1.00 | 0.1640 |   2.55 KB |        1.00 |
| **SimpleReflectionWithJsonGuard** | Byte[909] | 2.155 μs | 0.0257 μs | 0.0228 μs |  1.04 | 0.1640 |   2.55 KB |        1.00 |
| SimpleReflectionWithJsonRequired  | Byte[909] | 2.256 μs | 0.0148 μs | 0.0138 μs |  1.09 | 0.1907 |   2.93 KB |        1.15 |
| SimpleReflectionWithRequired      | Byte[909] | 2.271 μs | 0.0229 μs | 0.0214 μs |  1.10 | 0.1907 |   2.93 KB |        1.15 |
</details>

<details>
<summary>Simple, source generated</summary>

| Method                           | json      | Mean     | Error     | StdDev    | Ratio | Gen0   | Allocated | Alloc Ratio |
|--------------------------------- |---------- |---------:|----------:|----------:|------:|-------:|----------:|------------:|
| SimpleSourceGen                  | Byte[909] | 2.094 μs | 0.0135 μs | 0.0126 μs |  1.00 | 0.1640 |   2.55 KB |        1.00 |
| **SimpleSourceGenWithJsonGuard** | Byte[909] | 2.082 μs | 0.0195 μs | 0.0183 μs |  0.99 | 0.1640 |   2.55 KB |        1.00 |
| SimpleSourceGenWithJsonRequired  | Byte[909] | 2.242 μs | 0.0199 μs | 0.0186 μs |  1.07 | 0.1907 |   2.93 KB |        1.15 |
| SimpleSourceGenWithRequired      | Byte[909] | 2.890 μs | 0.0249 μs | 0.0233 μs |  1.38 | 0.2251 |   3.45 KB |        1.35 |
</details>

#### Native AOT

<details>
<summary>Host Environment</summary>

```
BenchmarkDotNet v0.15.8, Linux Ubuntu 24.04.4 LTS (Noble Numbat)
12th Gen Intel Core i9-12900K 3.19GHz, 1 CPU, 24 logical and 12 physical cores
.NET SDK 10.0.401
  [Host]         : .NET 10.0.12 (10.0.12, 10.0.1226.42308), X64 RyuJIT x86-64-v3
  NativeAOT 10.0 : .NET 10.0.12, X64 NativeAOT x86-64-v3

Job=NativeAOT 10.0  Runtime=NativeAOT 10.0  Toolchain=Latest ILCompiler
```

</details>

<details>
<summary>Complex, source generated</summary>

| Method                            | json          | Mean     | Error     | StdDev    | Ratio | RatioSD | Gen0     | Gen1     | Allocated | Alloc Ratio |
|---------------------------------- |-------------- |---------:|----------:|----------:|------:|--------:|---------:|---------:|----------:|------------:|
| ComplexSourceGen                  | Byte[1872295] | 4.794 ms | 0.0580 ms | 0.0543 ms |  1.00 |    0.02 | 148.4375 | 109.3750 |   2.24 MB |        1.00 |
| **ComplexSourceGenWithJsonGuard** | Byte[1872295] | 4.815 ms | 0.0298 ms | 0.0279 ms |  1.00 |    0.01 | 148.4375 | 109.3750 |   2.24 MB |        1.00 |
| ComplexSourceGenWithJsonRequired  | Byte[1872295] | 4.996 ms | 0.0457 ms | 0.0428 ms |  1.04 |    0.01 | 171.8750 | 164.0625 |   2.64 MB |        1.18 |
| ComplexSourceGenWithRequired      | Byte[1872295] | 8.106 ms | 0.0551 ms | 0.0515 ms |  1.69 |    0.02 | 218.7500 | 171.8750 |   3.35 MB |        1.50 |
</details>

<details>
<summary>Simple, source generated</summary>

| Method                           | json      | Mean     | Error     | StdDev    | Ratio | Gen0   | Allocated | Alloc Ratio |
|--------------------------------- |---------- |---------:|----------:|----------:|------:|-------:|----------:|------------:|
| SimpleSourceGen                  | Byte[909] | 3.032 μs | 0.0252 μs | 0.0236 μs |  1.00 | 0.1640 |   2.55 KB |        1.00 |
| **SimpleSourceGenWithJsonGuard** | Byte[909] | 3.057 μs | 0.0114 μs | 0.0107 μs |  1.01 | 0.1640 |   2.55 KB |        1.00 |
| SimpleSourceGenWithJsonRequired  | Byte[909] | 3.186 μs | 0.0177 μs | 0.0165 μs |  1.05 | 0.1907 |   2.93 KB |        1.15 |
| SimpleSourceGenWithRequired      | Byte[909] | 3.961 μs | 0.0263 μs | 0.0246 μs |  1.31 | 0.2213 |   3.45 KB |        1.35 |
</details>

## License

This project is released under the [**MIT License**](LICENSE).
