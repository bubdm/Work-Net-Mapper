namespace WorkMapper.Functions
{
    public interface IValueResolver<in TSource, in TDestination, out TDestinationMember>
    {
        TDestinationMember Resolve(TSource source, TDestination destination);
    }

    public interface IValueResolver<in TSource, in TDestination, out TDestinationMember, in TContext>
    {
        TDestinationMember Resolve(TSource source, TDestination destination, TContext context);
    }
}
