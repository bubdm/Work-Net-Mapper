using System;

namespace WorkMapper
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new MapperConfig();
            config.CreateMap<SourceData, DestinationData>();

            var mapper = config.ToMapper();

            var destination = mapper.Map<SourceData, DestinationData>(new SourceData { Value = 1 });
        }
    }

    public class SourceData
    {
        public int Value { get; set; }
    }

    public class DestinationData
    {
        public int Value { get; set; }
    }}
