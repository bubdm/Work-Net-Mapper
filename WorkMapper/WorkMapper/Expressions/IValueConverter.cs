namespace WorkMapper.Expressions
{
    public interface IValueConverter<in TSourceMember, out TDestinationMember>
    {
        TDestinationMember Convert(TSourceMember value);
    }

    public interface IValueConverter<in TSourceMember, out TDestinationMember, in TContext>
    {
        TDestinationMember Convert(TSourceMember value, TContext context);
    }
}
