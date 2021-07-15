using WorkMapper.Functions;

namespace WorkMapper.Expressions
{
    using System;

    using WorkMapper.Components;

    public interface IDefaultExpression
    {
        //--------------------------------------------------------------------------------
        // Factory
        //--------------------------------------------------------------------------------

        IDefaultExpression FactoryUsing(IFactoryResolver resolver);

        IDefaultExpression FactoryUsing<TDestination>(Func<TDestination> factory);

        //--------------------------------------------------------------------------------
        // Convert
        //--------------------------------------------------------------------------------

        IDefaultExpression ConvertUsing(IConverterResolver resolver);

        IDefaultExpression ConvertUsing<TSourceMember, TDestinationMember>(Func<TSourceMember, TDestinationMember> converter);

        IDefaultExpression ConvertUsing<TSourceMember, TDestinationMember, TContext>(Func<TSourceMember, TContext, TDestinationMember> converter);

        IDefaultExpression ConvertUsing<TSourceMember, TDestinationMember>(IValueConverter<TSourceMember, TDestinationMember> converter);

        IDefaultExpression ConvertUsing<TSourceMember, TDestinationMember, TContext>(IValueConverter<TSourceMember, TDestinationMember, TContext> converter);

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
