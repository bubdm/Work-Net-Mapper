namespace WorkMapper
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    using Smart.Reflection;

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
        }

        public IMappingExpression<TSource, TDestination> CreateMap<TSource, TDestination>()
        {
            var option = new MappingOption(typeof(TSource), typeof(TDestination), null);
            entries.Add(new MapperEntry(null, option));
            return new MappingExpression<TSource, TDestination>(option);
        }

        public IMappingExpression<TSource, TDestination> CreateMap<TSource, TDestination>(string profile)
        {
            var option = new MappingOption(typeof(TSource), typeof(TDestination), null);
            entries.Add(new MapperEntry(profile, option));
            return new MappingExpression<TSource, TDestination>(option);
        }

        public IMappingExpression<TSource, TDestination, TContext> CreateMap<TSource, TDestination, TContext>()
        {
            var option = new MappingOption(typeof(TSource), typeof(TDestination), typeof(TContext));
            entries.Add(new MapperEntry(null, option));
            return new MappingExpression<TSource, TDestination, TContext>(option);
        }

        public IMappingExpression<TSource, TDestination, TContext> CreateMap<TSource, TDestination, TContext>(string profile)
        {
            var option = new MappingOption(typeof(TSource), typeof(TDestination), typeof(TContext));
            entries.Add(new MapperEntry(profile, option));
            return new MappingExpression<TSource, TDestination, TContext>(option);
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
            }
            else
            {
                MapperFactory = ReflectionHelper.IsCodegenAllowed ? EmitMapperFactory.Instance : ReflectionMapperFactory.Instance;
            }
            return this;
        }
    }
}
