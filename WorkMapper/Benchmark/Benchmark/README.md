``` ini

BenchmarkDotNet=v0.13.0, OS=Windows 10.0.19043.1083 (21H1/May2021Update)
AMD Ryzen 9 5900X, 1 CPU, 24 logical and 12 physical cores
.NET SDK=5.0.302
  [Host]    : .NET 5.0.8 (5.0.821.31504), X64 RyuJIT
  MediumRun : .NET 5.0.8 (5.0.821.31504), X64 RyuJIT

Job=MediumRun  IterationCount=15  LaunchCount=2  
WarmupCount=10  

```
|                      Method |      Mean |     Error |    StdDev |    Median |       Min |       Max |       P90 |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|---------------------------- |----------:|----------:|----------:|----------:|----------:|----------:|----------:|-------:|------:|------:|----------:|
|            SimpleAutoMapper | 66.236 ns | 0.3881 ns | 0.5566 ns | 66.294 ns | 65.318 ns | 67.261 ns | 67.003 ns | 0.0038 |     - |     - |      64 B |
|           SimpleAutoMapper2 | 69.743 ns | 0.2754 ns | 0.3676 ns | 69.644 ns | 69.310 ns | 70.780 ns | 70.204 ns | 0.0038 |     - |     - |      64 B |
|            SimpleTinyMapper | 26.712 ns | 0.0795 ns | 0.1191 ns | 26.688 ns | 26.465 ns | 26.966 ns | 26.884 ns | 0.0038 |     - |     - |      64 B |
|         SimpleInstantMapper | 87.780 ns | 0.1694 ns | 0.2375 ns | 87.752 ns | 87.301 ns | 88.246 ns | 88.105 ns | 0.0095 |     - |     - |     160 B |
|             SimpleRawMapper | 31.314 ns | 0.0474 ns | 0.0680 ns | 31.314 ns | 31.161 ns | 31.426 ns | 31.397 ns | 0.0038 |     - |     - |      64 B |
| SimpleInstantMapperWoLookup | 78.145 ns | 0.1279 ns | 0.1792 ns | 78.105 ns | 77.879 ns | 78.582 ns | 78.399 ns | 0.0095 |     - |     - |     160 B |
|     SimpleRawMapperWoLookup | 23.918 ns | 0.0898 ns | 0.1345 ns | 23.947 ns | 23.684 ns | 24.136 ns | 24.060 ns | 0.0038 |     - |     - |      64 B |
|                  SimpleHand |  6.859 ns | 0.0498 ns | 0.0682 ns |  6.831 ns |  6.782 ns |  7.033 ns |  6.954 ns | 0.0038 |     - |     - |      64 B |
|             MixedAutoMapper | 63.971 ns | 0.1867 ns | 0.2736 ns | 63.978 ns | 63.464 ns | 64.676 ns | 64.360 ns | 0.0038 |     - |     - |      64 B |
|             MixedTinyMapper | 41.772 ns | 0.1745 ns | 0.2558 ns | 41.862 ns | 41.358 ns | 42.198 ns | 42.105 ns | 0.0067 |     - |     - |     112 B |
|          MixedInstantMapper | 74.820 ns | 0.2223 ns | 0.3117 ns | 74.862 ns | 74.392 ns | 75.400 ns | 75.243 ns | 0.0124 |     - |     - |     208 B |
|              MixedRawMapper | 28.922 ns | 0.6806 ns | 0.9761 ns | 29.708 ns | 27.737 ns | 29.952 ns | 29.876 ns | 0.0038 |     - |     - |      64 B |
