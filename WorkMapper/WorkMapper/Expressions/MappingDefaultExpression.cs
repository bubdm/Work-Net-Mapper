namespace WorkMapper.Expressions
{
    using System;

    using WorkMapper.Components;
    using WorkMapper.Functions;
    using WorkMapper.Options;

    internal class MappingDefaultExpression : IMappingDefaultExpression
    {
        private readonly MappingOption option;

        public MappingDefaultExpression(MappingOption option)
        {
            this.option = option;
        }

        //--------------------------------------------------------------------------------
        // Converter
        //--------------------------------------------------------------------------------

        public IMappingDefaultExpression ConvertUsing(IConverterResolver resolver)
        {
            option.SetConverterResolver(resolver);
            return this;
        }

        public IMappingDefaultExpression ConvertUsing<TSourceMember, TDestinationMember>(Func<TSourceMember, TDestinationMember> converter)
        {
            option.SetConverter(converter);
            return this;
        }

        public IMappingDefaultExpression ConvertUsing<TSourceMember, TDestinationMember>(Func<TSourceMember, TDestinationMember, ResolutionContext> converter)
        {
            option.SetConverter(converter);
            return this;
        }

        public IMappingDefaultExpression ConvertUsing<TSourceMember, TDestinationMember>(IValueConverter<TSourceMember, TDestinationMember> converter)
        {
            option.SetConverter(converter);
            return this;
        }

        public IMappingDefaultExpression ConvertUsing<TSourceMember, TDestinationMember, TValueConverter>()
            where TValueConverter : IValueConverter<TSourceMember, TDestinationMember>
        {
            option.SetConverter<TSourceMember, TDestinationMember, TValueConverter>();
            return this;
        }

        //--------------------------------------------------------------------------------
        // Constant
        //--------------------------------------------------------------------------------

        public IMappingDefaultExpression Const<TMember>(TMember value)
        {
            option.SetConstValue(value);
            return this;
        }

        //--------------------------------------------------------------------------------
        // Null
        //--------------------------------------------------------------------------------

        public IMappingDefaultExpression NullIf<TMember>(TMember value)
        {
            option.SetNullIfValue(value);
            return this;
        }

        public IMappingDefaultExpression NullIgnore(Type type)
        {
            option.SetNullIgnore(type);
            return this;
        }
    }
}
