namespace WorkMapper.Expressions
{
    using System;

    public interface IConverterResolver
    {
        Func<TSourceMember, TDestinationMember> Resolve<TSourceMember, TDestinationMember>();
    }
}
