using WorkMapper.Options;

namespace WorkMapper.Mappers
{

    internal interface IMapperFactory
    {
        // TODO context?, to reference other type exists
        ObjectMapperInfo CreateInfo(DefaultOption defaultEntry, MappingOption entry);

        ContextObjectMapperInfo CreateContextInfo(DefaultOption defaultEntry, MappingOption entry);

        // TODO post process ?
    }
}
