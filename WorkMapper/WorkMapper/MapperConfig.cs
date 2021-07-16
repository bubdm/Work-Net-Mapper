namespace WorkMapper
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    using Smart.Converter;
    using Smart.Reflection;

    using WorkMapper.Components;
    using WorkMapper.Expressions;
    using WorkMapper.Handlers;
    using WorkMapper.Mappers;
    using WorkMapper.Options;

    public sealed class MapperConfig
    {
        private readonly List<MapperEntry> entries = new();

        internal DefaultOption DefaultOption { get; } = new();

        public List<IMissingHandler> MissingHandlers { get; } = new();

        [AllowNull]
        internal IMapperFactory MapperFactory { get; private set; }

        internal IEnumerable<MapperEntry> MapperOptions => entries;

        public MapperConfig()
        {
            SafeMode(false);
            DefaultOption.SetConverterResolver(new DefaultConverterResolver(ObjectConverter.Default));
        }

        public IMappingExpression<TSource, TDestination> CreateMap<TSource, TDestination>()
        {
            var option = new MappingOption(typeof(TSource), typeof(TDestination));
            entries.Add(new MapperEntry(null, option));
            return new MappingExpression<TSource, TDestination>(option);
        }

        public IMappingExpression<TSource, TDestination> CreateMap<TSource, TDestination>(string profile)
        {
            var option = new MappingOption(typeof(TSource), typeof(TDestination));
            entries.Add(new MapperEntry(profile, option));
            return new MappingExpression<TSource, TDestination>(option);
        }

        public MapperConfig Default(Action<IDefaultExpression> option)
        {
            option(new DefaultExpression(DefaultOption));
            return this;
        }

        internal MapperConfig SafeMode(bool value)
        {
            if (value)
            {
                MapperFactory = ReflectionMapperFactory.Instance;
                DefaultOption.SetFactoryResolver(new DefaultFactoryResolver(ReflectionDelegateFactory.Default));
            }
            else
            {
                MapperFactory = ReflectionHelper.IsCodegenAllowed ? EmitMapperFactory.Instance : ReflectionMapperFactory.Instance;
                DefaultOption.SetFactoryResolver(new DefaultFactoryResolver(DelegateFactory.Default));
            }
            return this;
        }
    }
}
