using System;

using WorkMapper.Options;

namespace WorkMapper.Handlers
{
    public interface IMissingHandler
    {
        MapperOption? Handle(Type sourceType, Type destinationType, Type? contextType);
    }
}
