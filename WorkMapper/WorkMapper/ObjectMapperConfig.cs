using System;
using WorkMapper.Expressions;

namespace WorkMapper
{
    public sealed class ObjectMapperConfig
    {
        // (Default)ObjectFactory
        // (Default)ConverterFactory
        // (ValueHolderSupport!) Fallback?, ILOnly?, Expression自動？

        public IMappingExpression<TSource, TDestination> CreateMap<TSource, TDestination>()
        {
            throw new NotImplementedException();
        }

        // TODO Config wide ? : Naming resolver Func<Dest, Source>, ...
    }
}
