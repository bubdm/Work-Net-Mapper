``` ini

BenchmarkDotNet=v0.13.0, OS=Windows 10.0.19043.1083 (21H1/May2021Update)
AMD Ryzen 9 5900X, 1 CPU, 24 logical and 12 physical cores
.NET SDK=5.0.302
  [Host]    : .NET 5.0.8 (5.0.821.31504), X64 RyuJIT
  MediumRun : .NET 5.0.8 (5.0.821.31504), X64 RyuJIT

Job=MediumRun  IterationCount=15  LaunchCount=2  
WarmupCount=10  

```
|                      Method |      Mean |     Error |    StdDev |       Min |       Max |       P90 |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|---------------------------- |----------:|----------:|----------:|----------:|----------:|----------:|-------:|------:|------:|----------:|
|            SimpleAutoMapper | 67.672 ns | 0.2757 ns | 0.3585 ns | 66.834 ns | 68.217 ns | 68.096 ns | 0.0038 |     - |     - |      64 B |
|           SimpleAutoMapper2 | 68.873 ns | 0.5297 ns | 0.7425 ns | 67.513 ns | 70.033 ns | 69.819 ns | 0.0038 |     - |     - |      64 B |
|            SimpleTinyMapper | 27.627 ns | 0.1534 ns | 0.2295 ns | 27.378 ns | 28.305 ns | 28.032 ns | 0.0038 |     - |     - |      64 B |
|         SimpleInstantMapper | 89.444 ns | 0.2104 ns | 0.3150 ns | 88.931 ns | 90.276 ns | 89.780 ns | 0.0095 |     - |     - |     160 B |
|             SimpleRawMapper | 33.457 ns | 0.3794 ns | 0.5561 ns | 32.414 ns | 34.922 ns | 34.061 ns | 0.0038 |     - |     - |      64 B |
| SimpleInstantMapperWoLookup | 79.510 ns | 0.1124 ns | 0.1576 ns | 79.237 ns | 79.868 ns | 79.711 ns | 0.0095 |     - |     - |     160 B |
|     SimpleRawMapperWoLookup | 24.875 ns | 0.0434 ns | 0.0594 ns | 24.777 ns | 25.053 ns | 24.944 ns | 0.0038 |     - |     - |      64 B |
|                  SimpleHand |  7.180 ns | 0.0125 ns | 0.0179 ns |  7.155 ns |  7.215 ns |  7.207 ns | 0.0038 |     - |     - |      64 B |
|             MixedAutoMapper | 65.156 ns | 1.1535 ns | 1.7265 ns | 63.038 ns | 68.195 ns | 67.349 ns | 0.0038 |     - |     - |      64 B |
|             MixedTinyMapper | 42.053 ns | 0.0991 ns | 0.1453 ns | 41.844 ns | 42.362 ns | 42.224 ns | 0.0067 |     - |     - |     112 B |
|          MixedInstantMapper | 76.375 ns | 0.2534 ns | 0.3552 ns | 75.912 ns | 77.239 ns | 76.792 ns | 0.0123 |     - |     - |     208 B |
|              MixedRawMapper | 29.507 ns | 0.2589 ns | 0.3713 ns | 29.046 ns | 30.339 ns | 29.907 ns | 0.0038 |     - |     - |      64 B |
