﻿namespace WorkMapper.Expressions
{
    using System;

    using WorkMapper.Functions;
    using WorkMapper.Options;

    internal class DefaultExpression : IDefaultExpression
    {
        private readonly DefaultOption defaultOption;

        public DefaultExpression(DefaultOption defaultOption)
        {
            this.defaultOption = defaultOption;
        }

        //--------------------------------------------------------------------------------
        // Factory
        //--------------------------------------------------------------------------------

        public IDefaultExpression FactoryUsingServiceProvider()
        {
            defaultOption.SetFactoryUseServiceProvider();
            return this;
        }

        public IDefaultExpression FactoryUsing<TDestination>(Func<TDestination> factory)
        {
            defaultOption.SetFactory(factory);
            return this;
        }

        public IDefaultExpression FactoryUsing<TDestination>(Func<ResolutionContext, TDestination> factory)
        {
            defaultOption.SetFactory(factory);
            return this;
        }

        //--------------------------------------------------------------------------------
        // Converter
        //--------------------------------------------------------------------------------

        public IDefaultExpression ConvertUsing<TSourceMember, TDestinationMember>(Func<TSourceMember, TDestinationMember> converter)
        {
            defaultOption.SetConverter(converter);
            return this;
        }

        public IDefaultExpression ConvertUsing<TSourceMember, TDestinationMember>(Func<TSourceMember, ResolutionContext, TDestinationMember> converter)
        {
            defaultOption.SetConverter(converter);
            return this;
        }

        public IDefaultExpression ConvertUsing<TSourceMember, TDestinationMember>(IValueConverter<TSourceMember, TDestinationMember> converter)
        {
            defaultOption.SetConverter(converter);
            return this;
        }

        public IDefaultExpression ConvertUsing<TSourceMember, TDestinationMember, TValueConverter>()
            where TValueConverter : IValueConverter<TSourceMember, TDestinationMember>
        {
            defaultOption.SetConverter<TSourceMember, TDestinationMember, TValueConverter>();
            return this;
        }

        //--------------------------------------------------------------------------------
        // Constant
        //--------------------------------------------------------------------------------

        public IDefaultExpression Const<TMember>(TMember value)
        {
            defaultOption.SetConstValue(value);
            return this;
        }

        //--------------------------------------------------------------------------------
        // Null
        //--------------------------------------------------------------------------------

        public IDefaultExpression NullIf<TMember>(TMember value)
        {
            defaultOption.SetNullIfValue(value);
            return this;
        }

        public IDefaultExpression NullIgnore(Type type)
        {
            defaultOption.SetNullIgnore(type);
            return this;
        }
    }
}
