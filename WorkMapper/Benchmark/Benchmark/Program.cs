namespace Benchmark
{
    using AutoMapper;

    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Columns;
    using BenchmarkDotNet.Configs;
    using BenchmarkDotNet.Diagnosers;
    using BenchmarkDotNet.Exporters;
    using BenchmarkDotNet.Jobs;
    using BenchmarkDotNet.Running;

    using Nelibur.ObjectMapper;

    public static class Program
    {
        public static void Main()
        {
            BenchmarkRunner.Run<MapperBenchmark>();
        }
    }

    public class BenchmarkConfig : ManualConfig
    {
        public BenchmarkConfig()
        {
            AddExporter(MarkdownExporter.Default, MarkdownExporter.GitHub);
            AddColumn(
                StatisticColumn.Mean,
                StatisticColumn.Min,
                StatisticColumn.Max,
                StatisticColumn.P90,
                StatisticColumn.Error,
                StatisticColumn.StdDev);
            AddDiagnoser(MemoryDiagnoser.Default);
            AddJob(Job.MediumRun);
        }
    }

    [Config(typeof(BenchmarkConfig))]
    public class MapperBenchmark
    {
        private readonly SimpleSource simpleSource = new SimpleSource();

        private readonly MixedSource mixedSource = new MixedSource();

        private IMapper mapper;

        private readonly ActionMapperFactory instantActionMapperFactory = new ActionMapperFactory();    // Boxed

        private readonly ActionMapperFactory rawActionMapperFactory = new ActionMapperFactory();

        private IActionMapper<SimpleSource, SimpleDestination> instantSimpleMapper;

        private IActionMapper<SimpleSource, SimpleDestination> rawSimpleMapper;

        [GlobalSetup]
        public void Setup()
        {
            // AutoMapper
            var config = new MapperConfiguration(c =>
            {
                c.CreateMap<SimpleSource, SimpleDestination>();
                c.CreateMap<MixedSource, MixedDestination>();
            });
            mapper = config.CreateMapper();

            // TinyMapper
            TinyMapper.Bind<SimpleSource, SimpleDestination>();
            TinyMapper.Bind<MixedSource, MixedDestination>();

            // Action based
            instantSimpleMapper = InstantMapperFactory.Create<SimpleSource, SimpleDestination>();
            instantActionMapperFactory.AddMapper(typeof(SimpleSource), typeof(SimpleDestination), instantSimpleMapper);
            instantActionMapperFactory.AddMapper(typeof(MixedSource), typeof(MixedDestination), InstantMapperFactory.Create<MixedSource, MixedDestination>());

            rawSimpleMapper = RawMapperFactory.CreateSimpleMapper();
            rawActionMapperFactory.AddMapper(typeof(SimpleSource), typeof(SimpleDestination), rawSimpleMapper);
            rawActionMapperFactory.AddMapper(typeof(MixedSource), typeof(MixedDestination), RawMapperFactory.CreateMixedMapper());
        }

        //--------------------------------------------------------------------------------
        // Simple
        //--------------------------------------------------------------------------------

        [Benchmark]
        public SimpleDestination SimpleAutoMapper() => mapper.Map<SimpleDestination>(simpleSource);


        [Benchmark]
        public SimpleDestination SimpleAutoMapper2() => mapper.Map<SimpleSource, SimpleDestination>(simpleSource);

        [Benchmark]
        public SimpleDestination SimpleTinyMapper() => TinyMapper.Map<SimpleDestination>(simpleSource);

        [Benchmark]
        public SimpleDestination SimpleInstantMapper() => instantActionMapperFactory.Map<SimpleDestination>(simpleSource);

        // Near Tiny
        [Benchmark]
        public SimpleDestination SimpleRawMapper() => rawActionMapperFactory.Map<SimpleDestination>(simpleSource);

        //--------------------------------------------------------------------------------
        // Without lookup
        //--------------------------------------------------------------------------------

        // Slow (object based/boxed & delegate getter/setter)
        [Benchmark]
        public SimpleDestination SimpleInstantMapperWoLookup() => instantSimpleMapper.Map(simpleSource);

        // Fast (No loop, No boxed)
        [Benchmark]
        public SimpleDestination SimpleRawMapperWoLookup() => rawSimpleMapper.Map(simpleSource);

        // Max
        [Benchmark]
        public SimpleDestination SimpleHand()
        {
            // Without Lookup
            var dest = new SimpleDestination
            {
                Value1 = simpleSource.Value1,
                Value2 = simpleSource.Value2,
                Value3 = simpleSource.Value3,
                Value4 = simpleSource.Value4,
                Value5 = simpleSource.Value5,
                Value6 = simpleSource.Value6,
                Value7 = simpleSource.Value7,
                Value8 = simpleSource.Value8
            };

            return dest;
        }

        //--------------------------------------------------------------------------------
        // Mixed
        //--------------------------------------------------------------------------------

        [Benchmark]
        public MixedDestination MixedAutoMapper() => mapper.Map<MixedDestination>(mixedSource);

        [Benchmark]
        public MixedDestination MixedTinyMapper() => TinyMapper.Map<MixedDestination>(mixedSource);

        [Benchmark]
        public MixedDestination MixedInstantMapper() => instantActionMapperFactory.Map<MixedDestination>(mixedSource);

        [Benchmark]
        public MixedDestination MixedRawMapper() => rawActionMapperFactory.Map<MixedDestination>(mixedSource);

        //--------------------------------------------------------------------------------
        // Convert
        //--------------------------------------------------------------------------------

        // TODO 2
    }

    // TODO +interface 1
}
