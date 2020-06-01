namespace WorkMapper.Expressions
{
    using System;
    using System.Collections.Generic;

    public interface IMemberDefaultExpression<in TMember>
    {
        // Default

        // Null
        IMemberDefaultExpression<TMember> NullIf(TMember value);

        IMemberDefaultExpression<TMember> NullIf(Type type, object value);

        IMemberDefaultExpression<TMember> NullIf(IDictionary<Type, object> values);

        // Constant
        IMemberDefaultExpression<TMember> Const(TMember value);


        IMemberDefaultExpression<TMember> Const(Type type, object value);

        // Convert
        IMemberDefaultExpression<TMember> ConvertUsing<TSourceMember, TDestinationMember>(IValueConverter<TSourceMember, TDestinationMember> converter);

        IMemberDefaultExpression<TMember> ConvertUsing<TSourceMember, TDestinationMember, TValueConverter>()
            where TValueConverter : IValueConverter<TSourceMember, TDestinationMember>;

        IMemberDefaultExpression<TMember> ConvertUsing(Type sourceType, Type destinationType, Type converterType);
    }
}
