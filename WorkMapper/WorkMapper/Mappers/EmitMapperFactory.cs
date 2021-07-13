using System;
using System.Collections.Generic;
using System.Text;

using WorkMapper.Options;

namespace WorkMapper.Mappers
{
    internal class EmitMapperFactory : IMapperFactory
    {
        public static EmitMapperFactory Instance { get; } = new();

        public ObjectMapperInfo CreateInfo(DefaultOption defaultEntry, MapperOption entry)
        {
            throw new NotImplementedException();
        }

        public ContextObjectMapperInfo CreateContextInfo(DefaultOption defaultEntry, MapperOption entry)
        {
            throw new NotImplementedException();
        }
    }
}
