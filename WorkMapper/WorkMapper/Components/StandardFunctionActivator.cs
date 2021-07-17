namespace WorkMapper.Components
{
    using System;

    public sealed class StandardFunctionActivator : IFunctionActivator
    {
        public object Activate(Type type)
        {
            return Activator.CreateInstance(type)!;
        }
    }
}
