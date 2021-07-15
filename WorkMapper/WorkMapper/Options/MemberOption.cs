namespace WorkMapper.Options
{
    using System.Reflection;

    using System.Linq.Expressions;

    public class MemberOption
    {
        public PropertyInfo Property { get; }

        public object? Converter { get; set; }

        public Expression? Expression { get; set; }

        public MemberOption(PropertyInfo property)
        {
            Property = property;
        }
    }
}
