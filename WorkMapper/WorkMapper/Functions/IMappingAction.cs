namespace WorkMapper.Functions
{
    public interface IMappingAction<in TSource, in TDestination>
    {
        void Process(TSource source, TDestination destination);
    }

    public interface IMappingAction<in TSource, in TDestination, in TContext>
    {
        void Process(TSource source, TDestination destination, TContext context);
    }
}
