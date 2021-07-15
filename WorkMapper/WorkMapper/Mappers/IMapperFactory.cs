using WorkMapper.Options;

namespace WorkMapper.Mappers
{

    internal interface IMapperFactory
    {
        // TODO context?, to reference other type exists
        object CreateInfo(IMapper mapper, DefaultOption defaultEntry, MappingOption entry);

        // TODO post process ?
    }
}
