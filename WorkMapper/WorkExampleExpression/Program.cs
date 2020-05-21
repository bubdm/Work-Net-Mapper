using System;
using AutoMapper;

namespace WorkExampleExpression
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new MapperConfiguration(c =>
            {
                c.CreateMap<SimpleSource, SimpleDestination>()
                    .ForMember(d => d.Value1, opt => opt.MapFrom(s => s.Value1));
            });
            var mapper = config.CreateMapper();        }
    }

    public class SimpleSource
    {
        public int Value1 { get; set; }
        public string Value2 { get; set; }
    }

    public class SimpleDestination
    {
        public int Value1 { get; set; }
        public string Value2 { get; set; }
    }
}
