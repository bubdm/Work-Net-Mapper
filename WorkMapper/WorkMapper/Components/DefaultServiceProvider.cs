namespace WorkMapper.Components
{
    using System;

    public sealed class DefaultServiceProvider : IServiceProvider
    {
        public object? GetService(Type serviceType) => Activator.CreateInstance(serviceType);
    }
}
