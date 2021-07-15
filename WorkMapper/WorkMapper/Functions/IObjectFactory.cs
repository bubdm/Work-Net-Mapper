namespace WorkMapper.Functions
{
    public interface IObjectFactory<out TDestination>
    {
        TDestination Create(ResolutionContext context);
    }
}
