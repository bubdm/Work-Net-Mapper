using System;

using WorkMapper.Options;

namespace WorkMapper.Handlers
{
    public interface IMissingHandler
    {
        MappingOption? Handle(Type sourceType, Type destinationType, Type? contextType);
    }
}
