using Smart.ComponentModel;

using WorkMapper.Components;
using WorkMapper.Options;

namespace WorkMapper.Mappers
{

    internal interface IMapperFactory
    {
        object CreateInfo(IMapper mapper, DefaultOption defaultOption, MappingOption mappingOption);
    }
}
