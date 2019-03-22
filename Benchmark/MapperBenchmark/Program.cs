using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

        private readonly ComplexDestination simpleDestination = new ComplexDestination();

        private readonly ActionRawMapper actionRawMapper = new ActionRawMapper();

        private readonly ActionBoxedRawMapper actionBoxedRawMapper = new ActionBoxedRawMapper();

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
        public void CustomNonTypedComplex()
        {
            mapperEntry.Map(simpleSource, simpleDestination);
        }

        [Benchmark]
        public void CustomNonTypedComplexNop()
        {
            mapperEntry.MapNop(simpleSource, simpleDestination);
        }

        [Benchmark]
        public void CustomNonTypedComplexDummy()
        {
            mapperEntry.MapDummy(simpleSource, simpleDestination);
        }

        [Benchmark]
        public void ActionRawMapper()
        {
            actionRawMapper.Map(simpleSource, simpleDestination);
        }

        [Benchmark]
        public void ActionBoxedRawMapper()
        {
            actionBoxedRawMapper.Map(simpleSource, simpleDestination);
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

    public sealed class ActionRawMapper
    {
        private readonly Action<ComplexSource, ComplexDestination>[] actions;

        public ActionRawMapper()
        {
            actions = new Action<ComplexSource, ComplexDestination>[]
            {
                (s, d) => { d.StringValue = s.StringValue; },
                (s, d) => { d.IntValue = s.IntValue; },
                (s, d) => { d.LongValue = s.LongValue; },
                (s, d) => { d.NullableIntValue = s.NullableIntValue; },
                (s, d) => { d.FloatValue = s.FloatValue; },
                (s, d) => { d.DateTimeValue = s.DateTimeValue; },
            };
        }

        public void Map(ComplexSource source, ComplexDestination destination)
        {
            for (var i = 0; i < actions.Length; i++)
            {
                actions[i](source, destination);
            }
        }
    }

    public sealed class ActionBoxedRawMapper
    {
        private readonly Action<ComplexSource, ComplexDestination>[] actions;

        public ActionBoxedRawMapper()
        {
            actions = new Action<ComplexSource, ComplexDestination>[]
            {
                (s, d) => { d.StringValue = (string)Wrap(s.StringValue); },
                (s, d) => { d.IntValue = (int)Wrap(s.IntValue); },
                (s, d) => { d.LongValue = (long)Wrap(s.LongValue); },
                (s, d) => { d.NullableIntValue = (int?)Wrap(s.NullableIntValue); },
                (s, d) => { d.FloatValue = (float)Wrap(s.FloatValue); },
                (s, d) => { d.DateTimeValue = (DateTime)Wrap(s.DateTimeValue); },
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static object Wrap<T>(T source)
        {
            return source;
        }

        public void Map(ComplexSource source, ComplexDestination destination)
        {
            for (var i = 0; i < actions.Length; i++)
            {
                actions[i](source, destination);
            }
        }
    }

    // --------------------------------------------------------------------------------

    public interface IMapperAction<in TSource, in TDestination>
    {
        void Execute(TSource source, TDestination destination);
    }

    public sealed class DummyMapper<TSource, TDestination> : IMapperAction<TSource, TDestination>
    {
        public void Execute(TSource source, TDestination destination)
        {
        }
    }

    public sealed class MapperEntry<TSource, TDestination>
    {
        private readonly Func<TDestination> factory;

        private readonly Action<TSource, TDestination>[] mapActions;

        private readonly Action<TSource, TDestination>[] dummyActions0;

        private readonly IMapperAction<TSource, TDestination>[] dummyActions;

        public MapperEntry(Func<TDestination> factory, Action<TSource, TDestination>[] mapActions)
        {
            this.factory = factory;
            this.mapActions = mapActions;
            var nop = (Action<TSource, TDestination>)((s, d) => { });
            dummyActions0 = Enumerable.Range(0, mapActions.Length)
                .Select(x => nop)
                .ToArray();
            dummyActions = Enumerable.Range(0, mapActions.Length)
                .Select(x => new DummyMapper<TSource, TDestination>())
                .ToArray();
        }

        public void Map(TSource source, TDestination destination)
        {
            for (var i = 0; i < mapActions.Length; i++)
            {
                mapActions[i](source, destination);
            }
        }

        public TDestination Map(TSource source)
        {
            var destination = factory();
            for (var i = 0; i < mapActions.Length; i++)
            {
                mapActions[i](source, destination);
            }

            return destination;
        }

        public TDestination MapNop(TSource source)
        {
            var destination = factory();
            for (var i = 0; i < dummyActions.Length; i++)
            {
                dummyActions0[i](source, destination);
            }

            return destination;
        }

        public TDestination MapNop(TSource source, TDestination destination)
        {
            for (var i = 0; i < dummyActions.Length; i++)
            {
                dummyActions0[i](source, destination);
            }

            return destination;
        }

        public TDestination MapDummy(TSource source)
        {
            var destination = factory();
            for (var i = 0; i < dummyActions.Length; i++)
            {
                dummyActions[i].Execute(source, destination);
            }

            return destination;
        }

        public TDestination MapDummy(TSource source, TDestination destination)
        {
            for (var i = 0; i < dummyActions.Length; i++)
            {
                dummyActions[i].Execute(source, destination);
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

            var destinationProperties = ComparerEnumerable.ToDictionary(destinationType
                    .GetProperties(BindingFlags.Instance | BindingFlags.Public), x => x.Name, x => x);

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

    public sealed class TypedCopyActionFactory<TSource, TDestination, TProperty>
    {
        // TODO
    }

}
