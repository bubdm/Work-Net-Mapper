namespace WorkMapper.Mappers
{
    using System;

    internal sealed class TypeMapperInfo<TSource, TDestination> : ObjectMapperInfo
    {
        public readonly Action<TSource, TDestination> TypeMapAction;

        public readonly Func<TSource, TDestination> TypeMapFunc;

        public TypeMapperInfo(
            Action<TSource, TDestination> typeMapAction,
            Func<TSource, TDestination> typeMapFunc,
            Action<object, object> objectMapAction,
            Func<object, object> objectMapFunc)
            : base(objectMapAction, objectMapFunc)
        {
            TypeMapAction = typeMapAction;
            TypeMapFunc = typeMapFunc;
        }
    }

    internal sealed class ContextTypeMapperInfo<TSource, TDestination, TContext> : ContextObjectMapperInfo
    {
        public readonly Action<TSource, TDestination, TContext> TypeMapAction;

        public readonly Func<TSource, TContext, TDestination> TypeMapFunc;

        public ContextTypeMapperInfo(
            Action<TSource, TDestination, TContext> typeMapAction,
            Func<TSource, TContext, TDestination> typeMapFunc,
            Action<object, object, object> objectMapAction,
            Func<object, object, object> objectMapFunc)
            : base(objectMapAction, objectMapFunc)
        {
            TypeMapAction = typeMapAction;
            TypeMapFunc = typeMapFunc;
        }
    }
}