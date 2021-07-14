namespace WorkMapper.Expressions
{
    using System;

    public interface IDefaultExpression
    {
        //--------------------------------------------------------------------------------
        // Factory
        //--------------------------------------------------------------------------------

        IDefaultExpression FactoryUsing<TDestination>(Func<TDestination> factory);

        IDefaultExpression FactoryUsing(IObjectFactory factory);

        //--------------------------------------------------------------------------------
        // Null
        //--------------------------------------------------------------------------------

        IDefaultExpression NullIf<TMember>(TMember value);

        IDefaultExpression NullIgnore(Type type);

        //--------------------------------------------------------------------------------
        // Constant
        //--------------------------------------------------------------------------------

        IDefaultExpression Const<TMember>(TMember value);

        //--------------------------------------------------------------------------------
        // Convert
        //--------------------------------------------------------------------------------

        IDefaultExpression ConvertUsing<TSourceMember, TDestinationMember>(Func<TSourceMember, TDestinationMember> converter);

        IDefaultExpression ConvertUsing<TSourceMember, TDestinationMember, TContext>(Func<TSourceMember, TDestinationMember, TContext> converter);

        IDefaultExpression ConvertUsing<TSourceMember, TDestinationMember>(IValueConverter<TSourceMember, TDestinationMember> converter);

        IDefaultExpression ConvertUsing<TSourceMember, TDestinationMember, TContext>(IValueConverter<TSourceMember, TDestinationMember, TContext> converter);
    }
}
