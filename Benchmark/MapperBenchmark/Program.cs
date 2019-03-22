using System.Collections.Generic;
using Smart.Collections.Generic;
using Smart.Converter;
using Smart.Reflection;

namespace MapperBenchmark
{
    using System;
    using System.Reflection;

    using AutoMapper;

    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Configs;
    using BenchmarkDotNet.Diagnosers;
    using BenchmarkDotNet.Exporters;
    using BenchmarkDotNet.Jobs;
    using BenchmarkDotNet.Running;

    public static class Program
    {
        public static void Main(string[] args)
        {
            BenchmarkRunner.Run<Benchmark>();
        }
    }

    public class BenchmarkConfig : ManualConfig
    {
        public BenchmarkConfig()
        {
            Add(MarkdownExporter.Default, MarkdownExporter.GitHub);
            Add(MemoryDiagnoser.Default);
            Add(Job.ShortRun);
        }
    }

    [Config(typeof(BenchmarkConfig))]
    public class Benchmark
    {
        private readonly ComplexSource simpleSource = new ComplexSource();

        private IMapper mapper;

        private MapperEntry<ComplexSource, ComplexDestination> mapperEntry;

        [GlobalSetup]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ComplexSource, ComplexDestination>();
            });

            mapper = config.CreateMapper();

            mapperEntry = MapperFactory.CreateMapper<ComplexSource, ComplexDestination>();
        }

        //[Benchmark]
        //public ComplexDestination AutoMapperComplex()
        //{
        //    return mapper.Map<ComplexSource, ComplexDestination>(simpleSource);
        //}

        [Benchmark]
        public ComplexDestination CustomNonTypedComplex()
        {
            return (ComplexDestination)mapperEntry.Map(simpleSource);
        }

        // TODO ToString, Parse, Complex, Array, SubObjectEquals?
    }

    public class ComplexSource
    {
        public string StringValue { get; set; }

        public int IntValue { get; set; }

        public long LongValue { get; set; }

        public int? NullableIntValue { get; set; }

        public float FloatValue { get; set; }

        public DateTime DateTimeValue { get; set; }
    }

    public class ComplexDestination
    {
        public string StringValue { get; set; }

        public int IntValue { get; set; }

        public long LongValue { get; set; }

        public int? NullableIntValue { get; set; }

        public float FloatValue { get; set; }

        public DateTime DateTimeValue { get; set; }
    }

    // --------------------------------------------------------------------------------

    public sealed class TypedMapperEntry<TSource, TDestination>
    {
        private readonly Action<TSource, TDestination>[] mapActions;

        public TypedMapperEntry(Action<TSource, TDestination>[] mapActions)
        {
            this.mapActions = mapActions;
        }

        public void Map(TSource source, TDestination destination)
        {
            for (var i = 0; i < mapActions.Length; i++)
            {
                mapActions[i](source, destination);
            }
        }
    }

    public sealed class MapperEntry<TSource, TDestination>
    {
        private readonly Func<TDestination> factory;

        private readonly Action<TSource, TDestination>[] mapActions;

        public MapperEntry(Func<TDestination> factory, Action<TSource, TDestination>[] mapActions)
        {
            this.factory = factory;
            this.mapActions = mapActions;
        }

        public void Map(TSource source, TDestination destination)
        {
            for (var i = 0; i < mapActions.Length; i++)
            {
                mapActions[i](source, destination);
            }
        }

        public object Map(TSource source)
        {
            var destination = factory();
            for (var i = 0; i < mapActions.Length; i++)
            {
                mapActions[i](source, destination);
            }

            return destination;
        }
    }

    public static class MapperFactory
    {
        public static MapperEntry<TSource, TDestination> CreateMapper<TSource, TDestination>()
        {
            var sourceType = typeof(TSource);
            var destinationType = typeof(TDestination);

            var destinationProperties = destinationType
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .ToDictionary(x => x.Name, x => x);

            // TODO typed
            var destinationFactory = DelegateFactory.Default.CreateFactory<TDestination>();

            var actions = new List<Action<TSource, TDestination>>();
            foreach (var sourcePi in sourceType.GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                if (!destinationProperties.TryGetValue(sourcePi.Name, out var destinationPi))
                {
                    continue;
                }

                // TODO typed
                var sourceGetter = DelegateFactory.Default.CreateGetter(sourcePi);
                var destinationSetter = DelegateFactory.Default.CreateSetter(destinationPi);

                if (sourcePi.PropertyType.IsAssignableFrom(destinationPi.PropertyType))
                {
                    // TODO Typed
                    // action class (non convert)<TS, TD, TP>
                    actions.Add((s, d) =>
                    {
                        destinationSetter(d, sourceGetter(s));
                    });
                }
                else
                {
                    // TODO Typed
                    // (need typed converter)
                    // action class (convert)<TS, TD, TSP, TDP>
                    var converter = ObjectConverter.Default.CreateConverter(sourcePi.PropertyType, destinationPi.PropertyType);
                    actions.Add((s, d) =>
                    {
                        destinationSetter(d, converter(sourceGetter(s)));
                    });
                }
            }

            return new MapperEntry<TSource, TDestination>(destinationFactory, actions.ToArray());
        }
    }
}
