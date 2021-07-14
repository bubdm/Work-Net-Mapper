using System;

using WorkMapper.Options;

namespace WorkMapper.Mappers
{
    internal class ReflectionMapperFactory: IMapperFactory
    {
        public static ReflectionMapperFactory Instance { get; } = new();


        public ObjectMapperInfo CreateInfo(DefaultOption defaultEntry, MappingOption entry)
        {
            throw new NotImplementedException();
        }

        public ContextObjectMapperInfo CreateContextInfo(DefaultOption defaultEntry, MappingOption entry)
        {
            throw new NotImplementedException();
        }
    }
}
