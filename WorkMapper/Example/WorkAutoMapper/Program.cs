using System;

using AutoMapper;

namespace WorkAutoMapper
{
    class Program
    {
        static void Main(string[] args)
        {
            var autoMapperConfig = new MapperConfiguration(c =>
            {
                c.CreateMap<long, int>().ConstructUsing(x => (int)x + 1);
                c.CreateMap<long?, int>().ConstructUsing(x => x.HasValue ? (int)x + 2 : -1);
                c.CreateMap<long?, int?>().ConstructUsing(x => x.HasValue ? (int)x + 3 : -2);
                c.CreateMap<long, int?>().ConstructUsing(x => (int)x + 4);
                c.CreateMap<Source, Destination>();
            });
            var autoMapper = autoMapperConfig.CreateMapper();

            var d = autoMapper.Map<Source, Destination>(new Source { Value = 1 });
        }
    }

    public class Source
    {
        public long? Value { get; set; }
    }

    public class Destination
    {
        public int Value { get; set; }
    }
}
