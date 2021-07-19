namespace WorkMapper
{
    public static class Program
    {
        public static void Main()
        {
            var config = new MapperConfig();
            config.CreateMap<SourceData, DestinationData>();

            var mapper = config.ToMapper();

            var source = new SourceData { Value = 1 };
            var destination = mapper.Map<SourceData, DestinationData>(source);
            mapper.Map(source, destination);
            destination = mapper.Map<SourceData, DestinationData>(source, string.Empty);
            mapper.Map(source, destination, string.Empty);
        }
    }

    public class SourceData
    {
        public int Value { get; set; }
    }

    public class DestinationData
    {
        public int Value { get; set; }
    }
}
