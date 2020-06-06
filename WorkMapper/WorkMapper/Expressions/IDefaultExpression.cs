namespace WorkMapper.Expressions
{
    using System;
    using System.Collections.Generic;

    public interface IDefaultExpression
    {
        //--------------------------------------------------------------------------------
        // Null
        //--------------------------------------------------------------------------------

        IDefaultExpression NullIf<TMember>(TMember value);

        IDefaultExpression NullIf(IDictionary<Type, object> values);

        //--------------------------------------------------------------------------------
        // Constant
        //--------------------------------------------------------------------------------

        IDefaultExpression Const<TMember>(TMember value);

        IDefaultExpression Const(IDictionary<Type, object> values);

        //--------------------------------------------------------------------------------
        // Convert
        //--------------------------------------------------------------------------------

        IDefaultExpression ConvertUsing<TSourceMember, TDestinationMember>(IValueConverter<TSourceMember, TDestinationMember> converter);

        IDefaultExpression ConvertUsing<TSourceMember, TDestinationMember, TValueConverter>()
            where TValueConverter : IValueConverter<TSourceMember, TDestinationMember>;
    }
}
