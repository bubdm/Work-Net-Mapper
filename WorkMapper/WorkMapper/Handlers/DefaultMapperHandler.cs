using System;

using WorkMapper.Mappers;
using WorkMapper.Options;

namespace WorkMapper.Handlers
{
    public sealed class DefaultMapperHandler : IMissingHandler
    {
        public MapperOption? Handle(Type sourceType, Type destinationType, Type? contextType)
        {
            throw new NotImplementedException();
        }
    }
}
