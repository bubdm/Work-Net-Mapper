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

        public ObjectMapperConfig MemberDefault<TMember>(Action<IMemberDefaultExpression<TMember>> option)
        {
            // TODO Default
            return this;
        }
    }
}
