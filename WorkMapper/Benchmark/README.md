|                      Method |      Mean |    Error |   StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|---------------------------- |----------:|---------:|---------:|-------:|------:|------:|----------:|
|            SimpleAutoMapper | 149.06 ns | 4.041 ns | 6.048 ns | 0.0150 |     - |     - |      64 B |
|            SimpleTinyMapper |  57.54 ns | 1.359 ns | 2.033 ns | 0.0151 |     - |     - |      64 B |
|         SimpleInstantMapper | 108.75 ns | 6.437 ns | 9.232 ns | 0.0381 |     - |     - |     160 B |
|             SimpleRawMapper |  56.01 ns | 1.658 ns | 2.430 ns | 0.0153 |     - |     - |      64 B |
| SimpleInstantMapperWoLookup |  88.81 ns | 2.738 ns | 4.013 ns | 0.0381 |     - |     - |     160 B |
|     SimpleRawMapperWoLookup |  39.77 ns | 0.559 ns | 0.783 ns | 0.0153 |     - |     - |      64 B |
|                  SimpleHand |  14.29 ns | 0.551 ns | 0.824 ns | 0.0153 |     - |     - |      64 B |
|             MixedAutoMapper | 147.16 ns | 2.855 ns | 4.185 ns | 0.0150 |     - |     - |      64 B |
|             MixedTinyMapper |  92.84 ns | 4.294 ns | 6.427 ns | 0.0267 |     - |     - |     112 B |
|          MixedInstantMapper | 128.14 ns | 5.279 ns | 7.226 ns | 0.0496 |     - |     - |     208 B |
|              MixedRawMapper |  49.52 ns | 1.667 ns | 2.496 ns | 0.0153 |     - |     - |      64 B |
