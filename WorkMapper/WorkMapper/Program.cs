using System;
using System.Linq;

using WorkMapper.Functions;

namespace WorkMapper
{
    public static class Program
    {
        public static void Main()
        {
            var config = new MapperConfig()
                .AddDefaultMapper();
            //config.CreateMap<SourceData, DestinationData>();

            var mapper = config.ToMapper();

            var destination = mapper.Map<SourceData, DestinationData>(new SourceData { Value = 1 });
            mapper.Map(new SourceData { Value = 2 }, destination);
            var destination2 = mapper.Map<SourceData, DestinationData>(new SourceData { Value = 3 }, string.Empty);
            mapper.Map(new SourceData { Value = 4 }, destination2, string.Empty);
        }
    }

    public class SourceData
    {
        public int Value { get; set; }
    }

    public class DestinationData
    {
        public int Value { get; set; }
    }

    public sealed class FullMapper1
    {
        public INestedMapper mapper;

        public IServiceProvider factory;

        public Action<SourceData, DestinationData> beforeMap1;
        public Action<SourceData, DestinationData, ResolutionContext> beforeMap2;
        public IMappingAction<SourceData, DestinationData> beforeMap3;

        public Action<SourceData, DestinationData> atferMap1;
        public Action<SourceData, DestinationData, ResolutionContext> atferMap2;
        public IMappingAction<SourceData, DestinationData> atferMap3;

        public DestinationData Map(SourceData source, string parameter)
        {
            var context = new ResolutionContext(parameter, mapper);
            var destination = (DestinationData)factory.GetService(typeof(DestinationData))!;

            beforeMap1(source, destination);
            beforeMap2(source, destination, context);
            beforeMap3.Process(source, destination, context);

            // TODO

            atferMap1(source, destination);
            atferMap2(source, destination, context);
            atferMap3.Process(source, destination, context);

            return destination;
        }
    }

    public sealed class FullMapper2
    {
        public Func<DestinationData> factory;

        public DestinationData Map(SourceData source)
        {
            var data = factory();
            return data;
        }
    }

    public sealed class FullMapper3
    {
        public Func<SourceData, DestinationData> factory;

        public DestinationData Map(SourceData source)
        {
            var data = factory(source);
            return data;
        }
    }

    public sealed class FullMapper4
    {
        public Func<ResolutionContext, DestinationData> factory;

        public INestedMapper mapper;

        public DestinationData Map(SourceData source)
        {
            var context = new ResolutionContext(null, mapper);
            var data = factory(context);
            return data;
        }
    }

    public sealed class FullMapper5
    {
        public Func<SourceData, ResolutionContext, DestinationData> factory;

        public INestedMapper mapper;

        public DestinationData Map(SourceData source)
        {
            var context = new ResolutionContext(null, mapper);
            var data = factory(source, context);
            return data;
        }
    }

    public sealed class FullMapper6
    {
        public IObjectFactory<SourceData, DestinationData> factory;

        public INestedMapper mapper;

        public DestinationData Map(SourceData source)
        {
            var context = new ResolutionContext(null, mapper);
            var data = factory.Create(source, context);
            return data;
        }
    }
}
