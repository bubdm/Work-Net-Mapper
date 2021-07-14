namespace WorkMapper.Handlers
{
    using System;

    using WorkMapper.Options;

    public sealed class DefaultMapperHandler : IMissingHandler
    {
        public MappingOption? Handle(Type sourceType, Type destinationType, Type? contextType)
        {
            if (contextType is not null)
            {
                return null;
            }

            return new MappingOption(sourceType, destinationType, null);
        }
    }
}
