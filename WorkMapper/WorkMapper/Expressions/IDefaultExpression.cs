﻿namespace WorkMapper.Expressions
{
    using System;

    using WorkMapper.Functions;

    public interface IDefaultExpression
    {
        //--------------------------------------------------------------------------------
        // Factory
        //--------------------------------------------------------------------------------

        IDefaultExpression FactoryUsingServiceProvider();

        IDefaultExpression FactoryUsing<TDestination>(Func<TDestination> factory);

        IDefaultExpression FactoryUsing<TDestination>(Func<ResolutionContext, TDestination> factory);

        //--------------------------------------------------------------------------------
        // Convert
        //--------------------------------------------------------------------------------

        IDefaultExpression ConvertUsing<TSourceMember, TDestinationMember>(Func<TSourceMember, TDestinationMember> converter);

        IDefaultExpression ConvertUsing<TSourceMember, TDestinationMember>(Func<TSourceMember, ResolutionContext, TDestinationMember> converter);

        IDefaultExpression ConvertUsing<TSourceMember, TDestinationMember>(IValueConverter<TSourceMember, TDestinationMember> converter);

        IDefaultExpression ConvertUsing<TSourceMember, TDestinationMember, TValueConverter>()
            where TValueConverter : IValueConverter<TSourceMember, TDestinationMember>;

        //--------------------------------------------------------------------------------
        // Constant
        //--------------------------------------------------------------------------------

        IDefaultExpression Const<TMember>(TMember value);

        //--------------------------------------------------------------------------------
        // Null
        //--------------------------------------------------------------------------------

        IDefaultExpression NullIf<TMember>(TMember value);

        IDefaultExpression NullIgnore(Type type);
    }
}
