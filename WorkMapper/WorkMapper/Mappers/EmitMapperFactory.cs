using System;
using System.Collections.Generic;
using System.Text;

using WorkMapper.Options;

namespace WorkMapper.Mappers
{
    internal class EmitMapperFactory : IMapperFactory
    {
        public static EmitMapperFactory Instance { get; } = new();

        public object CreateInfo(IMapper mapper, DefaultOption defaultEntry, MappingOption entry)
        {
            throw new NotImplementedException();
        }
    }
}
