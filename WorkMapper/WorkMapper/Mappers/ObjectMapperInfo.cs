namespace WorkMapper.Mappers
{
    using System;

    internal class ObjectMapperInfo
    {
        public readonly Action<object, object> ObjectMapAction;

        public readonly Func<object, object> ObjectMapFunc;

        public ObjectMapperInfo(
            Action<object, object> objectMapAction,
            Func<object, object> objectMapFunc)
        {
            ObjectMapAction = objectMapAction;
            ObjectMapFunc = objectMapFunc;
        }
    }

    internal class ContextObjectMapperInfo
    {
        public readonly Action<object, object, object> ObjectMapAction;

        public readonly Func<object, object, object> ObjectMapFunc;

        public ContextObjectMapperInfo(
            Action<object, object, object> objectMapAction,
            Func<object, object, object> objectMapFunc)
        {
            ObjectMapAction = objectMapAction;
            ObjectMapFunc = objectMapFunc;
        }
    }
}