namespace WorkMapper.Functions
{
    public interface IObjectFactory
    {
        TDestination Create<TDestination>();
    }

    public interface IObjectFactory<in TSource>
    {
        TDestination Create<TDestination>(TSource source);
    }

    public interface IObjectFactory<in TSource, in TContext>
    {
        TDestination Create<TDestination>(TSource source, TContext context);
    }
}
