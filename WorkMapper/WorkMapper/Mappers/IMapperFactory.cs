namespace WorkMapper.Mappers
{
    using WorkMapper.Metadata;

    internal interface IMapperFactory
    {
        // TODO context?, to reference other type exists
        ObjectMapperInfo Create(MapperEntry entry);

        // TODO post process ?
    }
}
