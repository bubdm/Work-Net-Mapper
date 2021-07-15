using System;

using WorkMapper.Options;

namespace WorkMapper.Mappers
{
    internal class ReflectionMapperFactory: IMapperFactory
    {
        public static ReflectionMapperFactory Instance { get; } = new();


        public object CreateInfo(IMapper mapper, DefaultOption defaultEntry, MappingOption entry)
        {
            throw new NotImplementedException();
        }
    }
}
