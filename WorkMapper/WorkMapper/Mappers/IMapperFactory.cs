namespace WorkMapper.Mappers
{
    using WorkMapper.Metadata;

    public interface IMapperFactory
    {
        // TODO context?, to reference other type exists
        IMapper Create(MapperEntry entry);

        // TODO post process ?
    }
}
