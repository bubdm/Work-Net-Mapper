namespace WorkMapper.Expressions
{
    public interface IValueResolver<in TSource, in TDestination, out TDestinationMember>
    {
        TDestinationMember Resolve(TSource source, TDestination destination, object context);
    }
}
