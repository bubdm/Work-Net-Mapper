``` ini

BenchmarkDotNet=v0.13.0, OS=Windows 10.0.19043.1081 (21H1/May2021Update)
AMD Ryzen 9 5900X, 1 CPU, 24 logical and 12 physical cores
.NET SDK=5.0.301
  [Host]   : .NET 5.0.7 (5.0.721.25508), X64 RyuJIT
  ShortRun : .NET 5.0.7 (5.0.721.25508), X64 RyuJIT

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
|                      Method |      Mean |     Error |    StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|---------------------------- |----------:|----------:|----------:|-------:|------:|------:|----------:|
|            SimpleAutoMapper | 70.602 ns |  6.207 ns | 0.3402 ns | 0.0038 |     - |     - |      64 B |
|            SimpleTinyMapper | 28.486 ns |  4.988 ns | 0.2734 ns | 0.0038 |     - |     - |      64 B |
|         SimpleInstantMapper | 94.286 ns | 15.712 ns | 0.8612 ns | 0.0095 |     - |     - |     160 B |
|             SimpleRawMapper | 33.154 ns |  5.312 ns | 0.2912 ns | 0.0038 |     - |     - |      64 B |
| SimpleInstantMapperWoLookup | 82.812 ns |  1.480 ns | 0.0811 ns | 0.0095 |     - |     - |     160 B |
|     SimpleRawMapperWoLookup | 25.494 ns |  4.606 ns | 0.2524 ns | 0.0038 |     - |     - |      64 B |
|                  SimpleHand |  7.517 ns |  2.084 ns | 0.1142 ns | 0.0038 |     - |     - |      64 B |
|             MixedAutoMapper | 68.074 ns | 18.278 ns | 1.0019 ns | 0.0038 |     - |     - |      64 B |
|             MixedTinyMapper | 44.615 ns |  6.912 ns | 0.3789 ns | 0.0067 |     - |     - |     112 B |
|          MixedInstantMapper | 79.426 ns | 24.458 ns | 1.3406 ns | 0.0124 |     - |     - |     208 B |
|              MixedRawMapper | 29.429 ns |  3.379 ns | 0.1852 ns | 0.0038 |     - |     - |      64 B |
