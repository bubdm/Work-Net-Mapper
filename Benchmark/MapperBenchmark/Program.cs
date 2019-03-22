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
        private readonly SimpleSource simpleSource = new SimpleSource();

        private IMapper mapper;

        private MapperEntry mapperEntry;

        [GlobalSetup]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SimpleSource, SimpleDestination>();
            });

            mapper = config.CreateMapper();

            mapperEntry = MapperFactory.CreateMapper<SimpleSource, SimpleDestination>();
        }

        [Benchmark]
        public SimpleDestination AutoMapperSimple()
        {
            return mapper.Map<SimpleSource, SimpleDestination>(simpleSource);
        }

        [Benchmark]
        public SimpleDestination CustomNonTypedSimple()
        {
            return (SimpleDestination)mapperEntry.Map(simpleSource);
        }

        // TODO ToString, Parse, Complex, Array, SubObjectEquals?
    }

    public class SimpleSource
    {
        public int Value { get; set; }
    }

    public class SimpleDestination
    {
        public int Value { get; set; }
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

    public sealed class MapperEntry
    {
        private readonly Func<object> factory;

        private readonly Action<object, object>[] mapActions;

        public MapperEntry(Func<object> factory, Action<object, object>[] mapActions)
        {
            this.factory = factory;
            this.mapActions = mapActions;
        }

        public void Map(object source, object destination)
        {
            for (var i = 0; i < mapActions.Length; i++)
            {
                mapActions[i](source, destination);
            }
        }

        public object Map(object source)
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
        // TODO Typed
        // (need typed converter)
        // action class (non convert)<TS, TD, TP> (convert)<TS, TD, TSP, TDP>

        public static MapperEntry CreateMapper<TSource, TDestination>()
        {
            var sourceType = typeof(TSource);
            var destinationType = typeof(TDestination);

            var destinationProperties = destinationType
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .ToDictionary(x => x.Name, x => x);

            var destinationFactory = DelegateFactory.Default.CreateFactory0(destinationType.GetConstructor(Type.EmptyTypes));

            var actions = new List<Action<object, object>>();
            foreach (var sourcePi in sourceType.GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                if (!destinationProperties.TryGetValue(sourcePi.Name, out var destinationPi))
                {
                    continue;
                }

                var sourceGetter = DelegateFactory.Default.CreateGetter(sourcePi);
                var destinationSetter = DelegateFactory.Default.CreateSetter(destinationPi);

                if (sourcePi.PropertyType.IsAssignableFrom(destinationPi.PropertyType))
                {
                    actions.Add((s, d) =>
                    {
                        destinationSetter(d, sourceGetter(s));
                    });
                }
                else
                {
                    var converter = ObjectConverter.Default.CreateConverter(sourcePi.PropertyType, destinationPi.PropertyType);
                    actions.Add((s, d) =>
                    {
                        destinationSetter(d, converter(sourceGetter(s)));
                    });
                }
            }

            return new MapperEntry(destinationFactory, actions.ToArray());
        }
    }
}
