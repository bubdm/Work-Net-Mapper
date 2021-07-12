//namespace WorkMapper.Expressions
//{
//    using System;

//    public interface ITypeDefaultExpression<in TMember>
//    {
//        //--------------------------------------------------------------------------------
//        // Null
//        //--------------------------------------------------------------------------------

//        ITypeDefaultExpression<TMember> NullIf(TMember value);

//        //--------------------------------------------------------------------------------
//        // Constant
//        //--------------------------------------------------------------------------------

//        ITypeDefaultExpression<TMember> Const(TMember value);

//        //--------------------------------------------------------------------------------
//        // Convert
//        //--------------------------------------------------------------------------------

//        ITypeDefaultExpression<TMember> ConvertUsing<TSourceMember, TDestinationMember>(IValueConverter<TSourceMember, TDestinationMember> converter);

//        ITypeDefaultExpression<TMember> ConvertUsing<TSourceMember, TDestinationMember, TValueConverter>()
//            where TValueConverter : IValueConverter<TSourceMember, TDestinationMember>;
//    }
//}
