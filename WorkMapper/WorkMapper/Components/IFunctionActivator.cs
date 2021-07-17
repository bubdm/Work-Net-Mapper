namespace WorkMapper.Components
{
    using System;

    public interface IFunctionActivator
    {
        object Activate(Type type);
    }
}
