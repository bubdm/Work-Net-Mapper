using System;

namespace WorkIL
{
    public class NestedMapper
    {
        public Func<SourceInner, DestinationInner> nestedClasMapper;

        public Func<StructSourceInner?, StructDestinationInner?> nestedNullableMapper;

        public Func<StructSourceInner, StructDestinationInner> nestedStructMapper;


        public void MapClass(Source s, Destination d)
        {
            if (s.Inner is not null)
            {
                d.Inner = nestedClasMapper(s.Inner);
            }
            else
            {
                d.Inner = null;
            }
        }

        public void MapClass2(Source s, Destination d)
        {
            d.Inner = s.Inner is not null ? nestedClasMapper(s.Inner) : null;
        }

        public void MapNullable(Source s, Destination d)
        {
            if (s.NullableInner is not null)
            {
                d.NullableInner = nestedNullableMapper(s.NullableInner);
            }
            else
            {
                d.NullableInner = null;
            }
        }

        public void MapNullable2(Source s, Destination d)
        {
            d.NullableInner = s.NullableInner is not null ? nestedNullableMapper(s.NullableInner) : null;
        }

        public void MapStruct(Source s, Destination d)
        {
            d.StructInner = nestedStructMapper(s.StructInner);
        }

        public struct StructSourceInner
        {
            public int Value { get; set; }
        }

        public struct StructDestinationInner
        {
            public int Value { get; set; }
        }

        public class SourceInner
        {
            public int Value { get; set; }
        }

        public class DestinationInner
        {
            public int Value { get; set; }
        }

        public class Source
        {
            public SourceInner? Inner { get; set; }

            public StructSourceInner? NullableInner { get; set; }

            public StructSourceInner StructInner { get; set; }
        }

        public class Destination
        {
            public DestinationInner? Inner { get; set; }

            public StructDestinationInner? NullableInner { get; set; }

            public StructDestinationInner StructInner { get; set; }
        }
    }
}
