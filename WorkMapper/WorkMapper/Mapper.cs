namespace WorkMapper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    using WorkMapper.Collections;
    using WorkMapper.Handlers;
    using WorkMapper.Mappers;
    using WorkMapper.Options;

    // TODO (ValueHolderSupport!) ILOnly?, Expression自動？
    public sealed class Mapper
    {
        private readonly MapperHashArray mapperCache = new(128);
        private readonly ContextMapperHashArray contextMapperCache = new(32);
        private readonly ProfileMapperHashArray profileMapperCache = new(32);
        private readonly ProfileContextMapperHashArray profileContextMapperCache = new(32);

        private readonly object sync = new();

        private readonly DefaultOption defaultOption;

        private readonly Dictionary<(string?, Type, Type, Type?), MappingOption> mapperOptions;

        private readonly IMissingHandler[] handlers;

        private readonly IMapperFactory factory;

        internal Mapper(MapperConfig config)
        {
            defaultOption = config.DefaultOption;
            mapperOptions = config.MapperOptions.ToDictionary(
                x => (x.Profile, x.Option.SourceType, x.Option.DestinationType, x.Option.ContextType),
                x => x.Option);
            handlers = config.MissingHandlers.ToArray();
            factory = config.MapperFactory;
        }

        //--------------------------------------------------------------------------------
        // Helper
        //--------------------------------------------------------------------------------

        private ObjectMapperInfo CreateTypeInfo(string? profile, Type sourceType, Type destinationType)
        {
            lock (sync)
            {
                if (!mapperOptions.TryGetValue((profile, sourceType, destinationType, null), out var mapperOption) &&
                    !String.IsNullOrEmpty(profile))
                {
                    mapperOption = handlers
                        .Select(x => x.Handle(sourceType, destinationType, null))
                        .FirstOrDefault(x => x is not null);
                    if (mapperOption is not null)
                    {
                        mapperOptions[(profile, sourceType, destinationType, null)] = mapperOption;
                    }
                }

                if (mapperOption is null)
                {
                    throw new InvalidOperationException(String.IsNullOrEmpty(profile)
                        ? $"Mapper not found. sourceType=[{sourceType}], destinationType=[{destinationType}]"
                        : $"Mapper not found. profile=[{profile}], sourceType=[{sourceType}], destinationType=[{destinationType}]");
                }

                return factory.CreateInfo(defaultOption, mapperOption);
            }
        }

        private ContextObjectMapperInfo CreateContextTypeInfo(string? profile, Type sourceType, Type destinationType, Type contextType)
        {
            lock (sync)
            {
                if (!mapperOptions.TryGetValue((profile, sourceType, destinationType, contextType), out var mapperOption) &&
                    !String.IsNullOrEmpty(profile))
                {
                    mapperOption = handlers
                        .Select(x => x.Handle(sourceType, destinationType, contextType))
                        .FirstOrDefault(x => x is not null);
                    if (mapperOption is not null)
                    {
                        mapperOptions[(profile, sourceType, destinationType, contextType)] = mapperOption;
                    }
                }

                if (mapperOption is null)
                {
                    throw new InvalidOperationException(String.IsNullOrEmpty(profile)
                        ? $"Mapper not found. sourceType=[{sourceType}], destinationType=[{destinationType}], contextType=[{contextType}]"
                        : $"Mapper not found. profile=[{profile}], sourceType=[{sourceType}], destinationType=[{destinationType}], contextType=[{contextType}]");
                }

                return factory.CreateContextInfo(defaultOption, mapperOption);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private TypeMapperInfo<TSource, TDestination> FindTypeInfo<TSource, TDestination>()
        {
            if (!mapperCache.TryGetValue(typeof(TSource), typeof(TDestination), out var info))
            {
                info = mapperCache.AddIfNotExist(typeof(TSource), typeof(TDestination), (ts, td) => CreateTypeInfo(null, ts, td));
            }

            return (TypeMapperInfo<TSource, TDestination>)info!;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ContextTypeMapperInfo<TSource, TDestination, TContext> FindContextTypeInfo<TSource, TDestination, TContext>()
        {
            if (!contextMapperCache.TryGetValue(typeof(TSource), typeof(TDestination), typeof(TContext), out var info))
            {
                info = contextMapperCache.AddIfNotExist(typeof(TSource), typeof(TDestination), typeof(TContext), (ts, td, tc) => CreateContextTypeInfo(null, ts, td, tc));
            }

            return (ContextTypeMapperInfo<TSource, TDestination, TContext>)info;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ObjectMapperInfo FindObjectInfo(Type sourceType, Type destinationType)
        {
            if (!mapperCache.TryGetValue(sourceType, destinationType, out var info))
            {
                info = mapperCache.AddIfNotExist(sourceType, destinationType, (ts, td) => CreateTypeInfo(null, ts, td));
            }

            return info;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ContextObjectMapperInfo FindContextObjectInfo(Type sourceType, Type destinationType, Type contextType)
        {
            if (!contextMapperCache.TryGetValue(sourceType, destinationType, contextType, out var info))
            {
                info = contextMapperCache.AddIfNotExist(sourceType, destinationType, contextType, (ts, td, tc) => CreateContextTypeInfo(null, ts, td, tc));
            }

            return info;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private TypeMapperInfo<TSource, TDestination> FindTypeInfo<TSource, TDestination>(string profile)
        {
            if (!profileMapperCache.TryGetValue(profile, typeof(TSource), typeof(TDestination), out var info))
            {
                // ReSharper disable once ConvertClosureToMethodGroup
                info = profileMapperCache.AddIfNotExist(profile, typeof(TSource), typeof(TDestination), (p, ts, td) => CreateTypeInfo(p, ts, td));
            }

            return (TypeMapperInfo<TSource, TDestination>)info;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ContextTypeMapperInfo<TSource, TDestination, TContext> FindContextTypeInfo<TSource, TDestination, TContext>(string profile)
        {
            if (!profileContextMapperCache.TryGetValue(profile, typeof(TSource), typeof(TDestination), typeof(TContext), out var info))
            {
                // ReSharper disable once ConvertClosureToMethodGroup
                info = profileContextMapperCache.AddIfNotExist(profile, typeof(TSource), typeof(TDestination), typeof(TContext), (p, ts, td, tc) => CreateContextTypeInfo(p, ts, td, tc));
            }

            return (ContextTypeMapperInfo<TSource, TDestination, TContext>)info;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ObjectMapperInfo FindObjectInfo(string profile, Type sourceType, Type destinationType)
        {
            if (!profileMapperCache.TryGetValue(profile, sourceType, destinationType, out var info))
            {
                // ReSharper disable once ConvertClosureToMethodGroup
                info = profileMapperCache.AddIfNotExist(profile, sourceType, destinationType, (p, ts, td) => CreateTypeInfo(p, ts, td));
            }

            return info;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ContextObjectMapperInfo FindContextObjectInfo(string profile, Type sourceType, Type destinationType, Type contextType)
        {
            if (!profileContextMapperCache.TryGetValue(profile, sourceType, destinationType, contextType, out var info))
            {
                // ReSharper disable once ConvertClosureToMethodGroup
                info = profileContextMapperCache.AddIfNotExist(profile, sourceType, destinationType, contextType, (p, ts, td, tc) => CreateContextTypeInfo(p, ts, td, tc));
            }

            return info;
        }

        //--------------------------------------------------------------------------------
        // Get
        //--------------------------------------------------------------------------------

        // Typed

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Action<TSource, TDestination> GetMapper<TSource, TDestination>() =>
            FindTypeInfo<TSource, TDestination>().TypeMapAction;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Action<TSource, TDestination, TContext> GetMapper<TSource, TDestination, TContext>() =>
            FindContextTypeInfo<TSource, TDestination, TContext>().TypeMapAction;

        // Object

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Action<object, object> GetMapper(Type sourceType, Type destinationType) =>
            FindObjectInfo(sourceType, destinationType).ObjectMapAction;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Action<object, object, object> GetContextMapper(Type sourceType, Type destinationType, Type contextType) =>
            FindContextObjectInfo(sourceType, destinationType, contextType).ObjectMapAction;

        // Typed with profile

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Action<TSource, TDestination> GetMapper<TSource, TDestination>(string profile) =>
            FindTypeInfo<TSource, TDestination>(profile).TypeMapAction;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Action<TSource, TDestination, TContext> GetMapper<TSource, TDestination, TContext>(string profile) =>
            FindContextTypeInfo<TSource, TDestination, TContext>(profile).TypeMapAction;

        // Object with profile

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Action<object, object> GetMapper(string profile, Type sourceType, Type destinationType) =>
            FindObjectInfo(profile, sourceType, destinationType).ObjectMapAction;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Action<object, object, object> GetContextMapper(string profile, Type sourceType, Type destinationType, Type contextType) =>
            FindContextObjectInfo(profile, sourceType, destinationType, contextType).ObjectMapAction;

        //--------------------------------------------------------------------------------
        // Map
        //--------------------------------------------------------------------------------

        // Typed

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TDestination Map<TSource, TDestination>(TSource source) =>
            FindTypeInfo<TSource, TDestination>().TypeMapFunc(source);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Map<TSource, TDestination>(TSource source, TDestination destination) =>
            FindTypeInfo<TSource, TDestination>().TypeMapAction(source, destination);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TDestination MapAlso<TSource, TDestination>(TSource source, TDestination destination)
        {
            FindTypeInfo<TSource, TDestination>().TypeMapAction(source, destination);
            return destination;
        }

        // Typed with context

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TDestination Map<TSource, TDestination, TContext>(TSource source, TContext context) =>
            FindContextTypeInfo<TSource, TDestination, TContext>().TypeMapFunc(source, context);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Map<TSource, TDestination, TContext>(TSource source, TDestination destination, TContext context) =>
            FindContextTypeInfo<TSource, TDestination, TContext>().TypeMapAction(source, destination, context);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TDestination MapAlso<TSource, TDestination, TContext>(TSource source, TDestination destination, TContext context)
        {
            FindContextTypeInfo<TSource, TDestination, TContext>().TypeMapAction(source, destination, context);
            return destination;
        }

        // Object

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public object Map(object source, Type sourceType, Type destinationType) =>
            FindObjectInfo(sourceType, destinationType).ObjectMapFunc(source);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Map(object source, object destination, Type sourceType, Type destinationType) =>
            FindObjectInfo(sourceType, destinationType).ObjectMapAction(source, destination);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public object MapAlso(object source, object destination, Type sourceType, Type destinationType)
        {
            FindObjectInfo(sourceType, destinationType).ObjectMapAction(source, destination);
            return destination;
        }

        // Object with context

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public object Map(Type sourceType, Type destinationType, Type contextType, object source, object context) =>
            FindContextObjectInfo(sourceType, destinationType, contextType).ObjectMapFunc(source, context);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Map(Type sourceType, Type destinationType, Type contextType, object source, object destination, object context) =>
            FindContextObjectInfo(sourceType, destinationType, contextType).ObjectMapAction(source, destination, context);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public object MapAlso(Type sourceType, Type destinationType, Type contextType, object source, object destination, object context)
        {
            FindContextObjectInfo(sourceType, destinationType, contextType).ObjectMapAction(source, destination, context);
            return destination;
        }

        // Typed with profile

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TDestination Map<TSource, TDestination>(string profile, TSource source) =>
            FindTypeInfo<TSource, TDestination>(profile).TypeMapFunc(source);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Map<TSource, TDestination>(string profile, TSource source, TDestination destination) =>
            FindTypeInfo<TSource, TDestination>(profile).TypeMapAction(source, destination);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TDestination MapAlso<TSource, TDestination>(string profile, TSource source, TDestination destination)
        {
            FindTypeInfo<TSource, TDestination>(profile).TypeMapAction(source, destination);
            return destination;
        }

        // Typed with profile with context

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TDestination Map<TSource, TDestination, TContext>(string profile, TSource source, TContext context) =>
            FindContextTypeInfo<TSource, TDestination, TContext>(profile).TypeMapFunc(source, context);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Map<TSource, TDestination, TContext>(string profile, TSource source, TDestination destination, TContext context) =>
            FindContextTypeInfo<TSource, TDestination, TContext>(profile).TypeMapAction(source, destination, context);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TDestination MapAlso<TSource, TDestination, TContext>(string profile, TSource source, TDestination destination, TContext context)
        {
            FindContextTypeInfo<TSource, TDestination, TContext>(profile).TypeMapAction(source, destination, context);
            return destination;
        }

        // Object with profile

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public object Map(string profile, object source, Type sourceType, Type destinationType) =>
            FindObjectInfo(profile, sourceType, destinationType).ObjectMapFunc(source);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Map(string profile, object source, object destination, Type sourceType, Type destinationType) =>
            FindObjectInfo(profile, sourceType, destinationType).ObjectMapAction(source, destination);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public object MapAlso(string profile, object source, object destination, Type sourceType, Type destinationType)
        {
            FindObjectInfo(profile, sourceType, destinationType).ObjectMapAction(source, destination);
            return destination;
        }

        // Object with profile with context

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public object Map(string profile, Type sourceType, Type destinationType, Type contextType, object source, object context) =>
            FindContextObjectInfo(profile, sourceType, destinationType, contextType).ObjectMapFunc(source, context);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Map(string profile, Type sourceType, Type destinationType, Type contextType, object source, object destination, object context) =>
            FindContextObjectInfo(profile, sourceType, destinationType, contextType).ObjectMapAction(source, destination, context);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public object MapAlso(string profile, Type sourceType, Type destinationType, Type contextType, object source, object destination, object context)
        {
            FindContextObjectInfo(profile, sourceType, destinationType, contextType).ObjectMapAction(source, destination, context);
            return destination;
        }
    }
}
