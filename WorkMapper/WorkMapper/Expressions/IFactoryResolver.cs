using System;

namespace WorkMapper.Expressions
{
    public interface IFactoryResolver
    {
        Func<T> Resolve<T>();
    }
}
