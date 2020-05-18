using AutoMapper;
using Nelibur.ObjectMapper;

namespace Benchmark
{
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Configs;
    using BenchmarkDotNet.Diagnosers;
    using BenchmarkDotNet.Exporters;
    using BenchmarkDotNet.Jobs;
    using BenchmarkDotNet.Running;

    public static class Program
    {
        public static void Main()
        {
            var b = new MapperBenchmark();
            b.Setup();
            b.SimpleInstantMapper();
            BenchmarkRunner.Run<MapperBenchmark>();
        }
    }

    public class BenchmarkConfig : ManualConfig
    {
        public BenchmarkConfig()
        {
            AddExporter(MarkdownExporter.Default, MarkdownExporter.GitHub);
            AddDiagnoser(MemoryDiagnoser.Default);
            //AddJob(Job.LongRun);
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

        private ActionMapper<SimpleSource, SimpleDestination> instantSimpleMapper;

        private ActionMapper<SimpleSource, SimpleDestination> rawSimpleMapper;

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
        public SimpleDestination SimpleTinyMapper() => TinyMapper.Map<SimpleDestination>(simpleSource);

        [Benchmark]
        public SimpleDestination SimpleInstantMapper() => instantActionMapperFactory.Map<SimpleDestination>(simpleSource);


        [Benchmark]
        public SimpleDestination SimpleRawMapper() => rawActionMapperFactory.Map<SimpleDestination>(simpleSource);

        [Benchmark]
        public SimpleDestination SimpleInstantMapperWoLookup() => instantSimpleMapper.Map(simpleSource);


        [Benchmark]
        public SimpleDestination SimpleRawMapperWoLookup() => rawSimpleMapper.Map(simpleSource);

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

        // TODO
    }
}
