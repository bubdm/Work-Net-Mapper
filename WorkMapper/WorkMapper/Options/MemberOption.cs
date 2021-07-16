namespace WorkMapper.Options
{
    using System;

    using System.Reflection;

    using System.Linq.Expressions;

    public class MemberOption
    {
        public PropertyInfo Property { get; }

        private bool ignore;

        private bool nested;

        private int order = Int32.MaxValue;

        private object? condition;

        private object? from;

        // TODO use
        private object? constantValue;

        // TODO use
        private object? nullIfValue;

        private object? converter;


        //public object? Converter { get; set; }

        //public Expression? Expression { get; set; }

        public MemberOption(PropertyInfo property)
        {
            Property = property;
        }

        //--------------------------------------------------------------------------------
        // Ignore
        //--------------------------------------------------------------------------------

        public void SetIgnore() => ignore = true;

        //--------------------------------------------------------------------------------
        // Nested
        //--------------------------------------------------------------------------------

        public void SetNested() => nested = true;

        //--------------------------------------------------------------------------------
        // Order
        //--------------------------------------------------------------------------------

        public void SetOrder(int value) => order = value;

        // TODO

        //--------------------------------------------------------------------------------
        // Internal
        //--------------------------------------------------------------------------------

        public bool IsIgnore() => ignore;

        public bool IsNested() => nested;

        public int GetOrder() => order;
    }
}
