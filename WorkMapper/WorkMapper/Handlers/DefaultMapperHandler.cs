﻿namespace WorkMapper.Handlers
{
    using System;

    using WorkMapper.Options;

    public sealed class DefaultMapperHandler : IMissingHandler
    {
        public int Priority { get; set; } = Int32.MinValue;

        public MappingOption Handle(Type sourceType, Type destinationType)
        {
            return new(sourceType, destinationType);
        }
    }
}
