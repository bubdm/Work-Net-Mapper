using System;

namespace WorkIL
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        public static void SetData(Data data)
        {
            data.StringValue = null;
            data.StringValue = string.Empty;
            data.StringValue = "123";

            data.ByteValue = 0;
            data.ByteValue = 10;
            data.ByteValue = 255;

            data.CharValue = '\0';
            data.CharValue = char.MaxValue;
            data.CharValue = char.MinValue;

            data.ShortValue = 0;
            data.ShortValue = 1;
            data.ShortValue = -1;
            data.ShortValue = 10;
            data.ShortValue = Int16.MaxValue;
            data.ShortValue = Int16.MinValue;

            data.IntValue = 0;
            data.IntValue = 1;
            data.IntValue = -1;
            data.IntValue = 10;
            data.IntValue = Int32.MaxValue;
            data.IntValue = Int32.MinValue;

            data.LongValue = 0L;
            data.LongValue = 1L;
            data.LongValue = -1L;
            data.LongValue = 10L;
            data.LongValue = Int64.MaxValue;
            data.LongValue = Int64.MinValue;

            data.UIntValue = 0U;
            data.UIntValue = 1U;
            data.UIntValue = 10U;
            data.UIntValue = UInt32.MaxValue;
            data.UIntValue = UInt32.MinValue;

            data.ULongValue = 0UL;
            data.ULongValue = 1UL;
            data.ULongValue = 10UL;
            data.ULongValue = UInt64.MaxValue;
            data.ULongValue = UInt64.MinValue;

            data.NullableIntValue = 0;
            data.NullableIntValue = 1;
            data.NullableIntValue = -1;
            data.NullableIntValue = 10;
            data.NullableIntValue = Int32.MaxValue;
            data.NullableIntValue = Int32.MinValue;
            data.NullableIntValue = null;

            data.FloatValue = 0f;
            data.FloatValue = 1f;
            data.FloatValue = 10f;
            data.FloatValue = Single.MaxValue;
            data.FloatValue = Single.MinValue;
            data.FloatValue = Single.NaN;
            data.FloatValue = Single.Epsilon;
            data.FloatValue = Single.PositiveInfinity;
            data.FloatValue = Single.NegativeInfinity;

            data.DoubleValue = 0d;
            data.DoubleValue = 1d;
            data.DoubleValue = 10d;
            data.DoubleValue = Double.MaxValue;
            data.DoubleValue = Double.MinValue;
            data.DoubleValue = Double.NaN;
            data.DoubleValue = Double.Epsilon;
            data.DoubleValue = Double.PositiveInfinity;
            data.DoubleValue = Double.NegativeInfinity;

            data.BoolValue = false;
            data.BoolValue = true;

            data.DecimalValue = 0m;
            data.DecimalValue = 1m;
            data.DecimalValue = -1m;
            data.DecimalValue = 10m;
            data.DecimalValue = Decimal.MaxValue;
            data.DecimalValue = Decimal.MinValue;
            data.DecimalValue = Decimal.Zero;
            data.DecimalValue = Decimal.One;
            data.DecimalValue = Decimal.MinusOne;

            data.IntPtrValue = IntPtr.Zero;
            data.IntPtrValue = IntPtr.MaxValue;
            data.IntPtrValue = IntPtr.MinValue;

            data.EnumValue = MyEnum.Zero;
            data.EnumValue = MyEnum.One;

            data.DateTimeValue = default;
            data.DateTimeValue = new DateTime(2000, 1, 1);
        }
    }

    public enum MyEnum
    {
        Zero,
        One
    }

    public class Data
    {
        public string StringValue { get; set; }
        public byte ByteValue { get; set; }
        public char CharValue { get; set; }
        public short ShortValue { get; set; }
        public int IntValue { get; set; }
        public long LongValue { get; set; }
        public uint UIntValue { get; set; }
        public ulong ULongValue { get; set; }
        public int? NullableIntValue { get; set; }
        public float FloatValue { get; set; }
        public double DoubleValue { get; set; }
        public bool BoolValue { get; set; }
        public decimal DecimalValue { get; set; }
        public IntPtr IntPtrValue { get; set; }
        public MyEnum EnumValue { get; set; }
        public DateTime DateTimeValue { get; set; }
    }
}
