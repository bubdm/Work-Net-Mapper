namespace WorkMapper.Components
{
    using System;

    using Smart.Reflection;

    public sealed class DefaultFactoryResolver : IFactoryResolver
    {
        private readonly IDelegateFactory delegateFactory;

        public DefaultFactoryResolver(IDelegateFactory delegateFactory)
        {
            this.delegateFactory = delegateFactory;
        }

        public Func<T> Resolve<T>() => delegateFactory.CreateFactory<T>();
    }
}
