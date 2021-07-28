namespace WorkIL
{
    public class Source
    {
        public int Value { get; set; }
    }

    public class Destination
    {
        public int Value { get; set; }
    }

    public class NullableSource
    {
        public int? Value { get; set; }

        public string? ClassValue { get; set; }
    }

    public class NullableDestination
    {
        public int? Value { get; set; }

        public string? ClassValue { get; set; }
    }
}
