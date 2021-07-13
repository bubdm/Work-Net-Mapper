namespace WorkMapper
{
    using System;
    using System.Runtime.CompilerServices;

    using WorkMapper.Collections;
    using WorkMapper.Mappers;

    public sealed class Mapper
    {
        // TODO Meta source tuple key normal dic ?

        private readonly MapperHashArray mapperCache = new(128);
        private readonly ContextMapperHashArray contextMapperCache = new(32);
        private readonly ProfileMapperHashArray profileMapperCache = new(32);
        private readonly ProfileContextMapperHashArray profileContextMapperCache = new(32);

        //--------------------------------------------------------------------------------
        // Helper
        //--------------------------------------------------------------------------------

        // TODO Helper2

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private TypeMapperInfo<TSource, TDestination> ResolveTypeInfo<TSource, TDestination>()
        {
            // TODO
            return null!;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ContextTypeMapperInfo<TSource, TDestination, TContext> ResolveContextTypeInfo<TSource, TDestination, TContext>()
        {
            // TODO
            return null!;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ObjectMapperInfo ResolveObjectInfo(Type sourceType, Type destinationType)
        {
            // TODO
            return null!;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ContextObjectMapperInfo ResolveContextObjectInfo(Type sourceType, Type destinationType, Type contextType)
        {
            // TODO
            return null!;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private TypeMapperInfo<TSource, TDestination> ResolveTypeInfo<TSource, TDestination>(string profile)
        {
            // TODO
            return null!;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ContextTypeMapperInfo<TSource, TDestination, TContext> ResolveContextTypeInfo<TSource, TDestination, TContext>(string profile)
        {
            // TODO
            return null!;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ObjectMapperInfo ResolveObjectInfo(string profile, Type sourceType, Type destinationType)
        {
            // TODO
            return null!;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ContextObjectMapperInfo ResolveContextObjectInfo(string profile, Type sourceType, Type destinationType, Type contextType)
        {
            // TODO
            return null!;
        }

        //--------------------------------------------------------------------------------
        // Get
        //--------------------------------------------------------------------------------

        // Typed

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Action<TSource, TDestination> GetMapper<TSource, TDestination>() =>
            ResolveTypeInfo<TSource, TDestination>().TypeMapAction;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Action<TSource, TDestination, TContext> GetMapper<TSource, TDestination, TContext>() =>
            ResolveContextTypeInfo<TSource, TDestination, TContext>().TypeMapAction;

        // Object

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Action<object, object> GetMapper(Type sourceType, Type destinationType) =>
            ResolveObjectInfo(sourceType, destinationType).ObjectMapAction;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Action<object, object, object> GetContextMapper(Type sourceType, Type destinationType, Type contextType) =>
            ResolveContextObjectInfo(sourceType, destinationType, contextType).ObjectMapAction;

        // Typed with profile

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Action<TSource, TDestination> GetMapper<TSource, TDestination>(string profile) =>
            ResolveTypeInfo<TSource, TDestination>(profile).TypeMapAction;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Action<TSource, TDestination, TContext> GetMapper<TSource, TDestination, TContext>(string profile) =>
            ResolveContextTypeInfo<TSource, TDestination, TContext>(profile).TypeMapAction;

        // Object with profile

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Action<object, object> GetMapper(string profile, Type sourceType, Type destinationType) =>
            ResolveObjectInfo(profile, sourceType, destinationType).ObjectMapAction;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Action<object, object, object> GetContextMapper(string profile, Type sourceType, Type destinationType, Type contextType) =>
            ResolveContextObjectInfo(profile, sourceType, destinationType, contextType).ObjectMapAction;

        //--------------------------------------------------------------------------------
        // Map
        //--------------------------------------------------------------------------------

        // Typed

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TDestination Map<TSource, TDestination>(TSource source) =>
            ResolveTypeInfo<TSource, TDestination>().TypeMapFunc(source);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Map<TSource, TDestination>(TSource source, TDestination destination) =>
            ResolveTypeInfo<TSource, TDestination>().TypeMapAction(source, destination);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TDestination MapAlso<TSource, TDestination>(TSource source, TDestination destination)
        {
            ResolveTypeInfo<TSource, TDestination>().TypeMapAction(source, destination);
            return destination;
        }

        // Typed with context

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TDestination Map<TSource, TDestination, TContext>(TSource source, TContext context) =>
            ResolveContextTypeInfo<TSource, TDestination, TContext>().TypeMapFunc(source, context);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Map<TSource, TDestination, TContext>(TSource source, TDestination destination, TContext context) =>
            ResolveContextTypeInfo<TSource, TDestination, TContext>().TypeMapAction(source, destination, context);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TDestination MapAlso<TSource, TDestination, TContext>(TSource source, TDestination destination, TContext context)
        {
            ResolveContextTypeInfo<TSource, TDestination, TContext>().TypeMapAction(source, destination, context);
            return destination;
        }

        // Object

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public object Map(object source, Type sourceType, Type destinationType) =>
            ResolveObjectInfo(sourceType, destinationType).ObjectMapFunc(source);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Map(object source, object destination, Type sourceType, Type destinationType) =>
            ResolveObjectInfo(sourceType, destinationType).ObjectMapAction(source, destination);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public object MapAlso(object source, object destination, Type sourceType, Type destinationType)
        {
            ResolveObjectInfo(sourceType, destinationType).ObjectMapAction(source, destination);
            return destination;
        }

        // Object with context

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public object Map(Type sourceType, Type destinationType, Type contextType, object source, object context) =>
            ResolveContextObjectInfo(sourceType, destinationType, contextType).ObjectMapFunc(source, context);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Map(Type sourceType, Type destinationType, Type contextType, object source, object destination, object context) =>
            ResolveContextObjectInfo(sourceType, destinationType, contextType).ObjectMapAction(source, destination, context);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public object MapAlso(Type sourceType, Type destinationType, Type contextType, object source, object destination, object context)
        {
            ResolveContextObjectInfo(sourceType, destinationType, contextType).ObjectMapAction(source, destination, context);
            return destination;
        }

        // Typed with profile

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TDestination Map<TSource, TDestination>(string profile, TSource source) =>
            ResolveTypeInfo<TSource, TDestination>(profile).TypeMapFunc(source);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Map<TSource, TDestination>(string profile, TSource source, TDestination destination) =>
            ResolveTypeInfo<TSource, TDestination>(profile).TypeMapAction(source, destination);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TDestination MapAlso<TSource, TDestination>(string profile, TSource source, TDestination destination)
        {
            ResolveTypeInfo<TSource, TDestination>(profile).TypeMapAction(source, destination);
            return destination;
        }

        // Typed with profile with context

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TDestination Map<TSource, TDestination, TContext>(string profile, TSource source, TContext context) =>
            ResolveContextTypeInfo<TSource, TDestination, TContext>(profile).TypeMapFunc(source, context);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Map<TSource, TDestination, TContext>(string profile, TSource source, TDestination destination, TContext context) =>
            ResolveContextTypeInfo<TSource, TDestination, TContext>(profile).TypeMapAction(source, destination, context);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TDestination MapAlso<TSource, TDestination, TContext>(string profile, TSource source, TDestination destination, TContext context)
        {
            ResolveContextTypeInfo<TSource, TDestination, TContext>(profile).TypeMapAction(source, destination, context);
            return destination;
        }

        // Object with profile

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public object Map(string profile, object source, Type sourceType, Type destinationType) =>
            ResolveObjectInfo(profile, sourceType, destinationType).ObjectMapFunc(source);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Map(string profile, object source, object destination, Type sourceType, Type destinationType) =>
            ResolveObjectInfo(profile, sourceType, destinationType).ObjectMapAction(source, destination);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public object MapAlso(string profile, object source, object destination, Type sourceType, Type destinationType)
        {
            ResolveObjectInfo(profile, sourceType, destinationType).ObjectMapAction(source, destination);
            return destination;
        }

        // Object with profile with context

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public object Map(string profile, Type sourceType, Type destinationType, Type contextType, object source, object context) =>
            ResolveContextObjectInfo(profile, sourceType, destinationType, contextType).ObjectMapFunc(source, context);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Map(string profile, Type sourceType, Type destinationType, Type contextType, object source, object destination, object context) =>
            ResolveContextObjectInfo(profile, sourceType, destinationType, contextType).ObjectMapAction(source, destination, context);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public object MapAlso(string profile, Type sourceType, Type destinationType, Type contextType, object source, object destination, object context)
        {
            ResolveContextObjectInfo(profile, sourceType, destinationType, contextType).ObjectMapAction(source, destination, context);
            return destination;
        }
    }
}
