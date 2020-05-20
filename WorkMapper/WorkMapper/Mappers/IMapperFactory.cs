using WorkMapper.Metadata;

namespace WorkMapper.Mappers
{
    public interface IMapperFactory
    {
        IMapper Create(MapperEntry entry);
    }
}
