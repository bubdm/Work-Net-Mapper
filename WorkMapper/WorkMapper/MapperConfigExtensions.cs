namespace WorkMapper
{
    using WorkMapper.Handlers;

    public static class MapperConfigExtensions
    {
        public static Mapper ToMapper(this MapperConfig config) => new(config);

        public static MapperConfig AddDefaultMapper(this MapperConfig config)
        {
            config.MissingHandlers.Add(new DefaultMapperHandler());
            return config;
        }
    }
}
