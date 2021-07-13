using WorkMapper.Options;

namespace WorkMapper.Mappers
{

    internal interface IMapperFactory
    {
        // TODO context?, to reference other type exists
        ObjectMapperInfo CreateInfo(DefaultOption defaultEntry, MapperOption entry);

        ContextObjectMapperInfo CreateContextInfo(DefaultOption defaultEntry, MapperOption entry);

        // TODO post process ?
    }
}
