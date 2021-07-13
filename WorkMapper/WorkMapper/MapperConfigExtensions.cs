namespace WorkMapper
{
    public static class MapperConfigExtensions
    {
        public static Mapper ToMapper(this MapperConfig config) => new(config);
    }
}
