namespace WorkMapper.Expressions
{
    using System;

    using WorkMapper.Components;
    using WorkMapper.Functions;
    using WorkMapper.Options;

    internal class DefaultExpression : IDefaultExpression
    {
        private readonly DefaultOption option;

        public DefaultExpression(DefaultOption option)
        {
            this.option = option;
        }

        //--------------------------------------------------------------------------------
        // Factory
        //--------------------------------------------------------------------------------

        public IDefaultExpression FactoryUsing(IFactoryResolver resolver)
        {
            option.SetFactoryResolver(resolver);
            return this;
        }

        public IDefaultExpression FactoryUsing<TDestination>(Func<TDestination> factory)
        {
            option.SetFactory(factory);
            return this;
        }

        //--------------------------------------------------------------------------------
        // Converter
        //--------------------------------------------------------------------------------

        public IDefaultExpression ConvertUsing(IConverterResolver resolver)
        {
            option.SetConverterResolver(resolver);
            return this;
        }

        public IDefaultExpression ConvertUsing<TSourceMember, TDestinationMember>(Func<TSourceMember, TDestinationMember> converter)
        {
            option.SetConverter(converter);
            return this;
        }

        public IDefaultExpression ConvertUsing<TSourceMember, TDestinationMember>(Func<TSourceMember, TDestinationMember, ResolutionContext> converter)
        {
            option.SetConverter(converter);
            return this;
        }

        public IDefaultExpression ConvertUsing<TSourceMember, TDestinationMember>(IValueConverter<TSourceMember, TDestinationMember> converter)
        {
            option.SetConverter(converter);
            return this;
        }

        public IDefaultExpression ConvertUsing<TSourceMember, TDestinationMember, TValueConverter>()
            where TValueConverter : IValueConverter<TSourceMember, TDestinationMember>
        {
            option.SetConverter<TSourceMember, TDestinationMember, TValueConverter>();
            return this;
        }

        //--------------------------------------------------------------------------------
        // Constant
        //--------------------------------------------------------------------------------

        public IDefaultExpression Const<TMember>(TMember value)
        {
            option.SetConstValue(value);
            return this;
        }

        //--------------------------------------------------------------------------------
        // Null
        //--------------------------------------------------------------------------------

        public IDefaultExpression NullIf<TMember>(TMember value)
        {
            option.SetNullIfValue(value);
            return this;
        }

        public IDefaultExpression NullIgnore(Type type)
        {
            option.SetNullIgnore(type);
            return this;
        }
    }
}
