namespace WorkMapper.Mappers
{
    using System;

    internal sealed class MapperInfo<TSource, TDestination>
    {
        public readonly Action<TSource, TDestination> MapAction;

        public readonly Func<TSource, TDestination> MapFunc;

        public readonly Action<TSource, TDestination, object> ParameterMapAction;

        public readonly Func<TSource, object, TDestination> ParameterMapFunc;

        public MapperInfo(
            Action<TSource, TDestination> mapAction,
            Func<TSource, TDestination> mapFunc,
            Action<TSource, TDestination, object> parameterMapAction,
            Func<TSource, object, TDestination> parameterMapFunc)
        {
            MapAction = mapAction;
            MapFunc = mapFunc;
            ParameterMapAction = parameterMapAction;
            ParameterMapFunc = parameterMapFunc;
        }
    }
}