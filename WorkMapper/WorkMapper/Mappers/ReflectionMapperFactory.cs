using System;

using Smart.ComponentModel;

using WorkMapper.Components;
using WorkMapper.Options;

namespace WorkMapper.Mappers
{
    internal class ReflectionMapperFactory: IMapperFactory
    {
        // IConverterResolver, IFactoryResolver, IFunctionActivatorはDI


        public object CreateInfo(IMapper mapper, DefaultOption defaultOption, MappingOption mappingOption)
        {
            throw new NotImplementedException();
        }
    }
}
