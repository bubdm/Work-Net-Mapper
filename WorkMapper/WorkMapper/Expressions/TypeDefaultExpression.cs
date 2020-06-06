namespace WorkMapper.Expressions
{
    using System;

    internal class TypeDefaultExpression<TMember> : ITypeDefaultExpression<TMember>
    {
        //--------------------------------------------------------------------------------
        // Null
        //--------------------------------------------------------------------------------

        public ITypeDefaultExpression<TMember> NullIf(TMember value)
        {
            throw new NotImplementedException();
        }

        //--------------------------------------------------------------------------------
        // Constant
        //--------------------------------------------------------------------------------

        public ITypeDefaultExpression<TMember> Const(TMember value)
        {
            throw new NotImplementedException();
        }

        //--------------------------------------------------------------------------------
        // Convert
        //--------------------------------------------------------------------------------

        public ITypeDefaultExpression<TMember> ConvertUsing<TSourceMember, TDestinationMember>(IValueConverter<TSourceMember, TDestinationMember> converter)
        {
            throw new NotImplementedException();
        }

        public ITypeDefaultExpression<TMember> ConvertUsing<TSourceMember, TDestinationMember, TValueConverter>() where TValueConverter : IValueConverter<TSourceMember, TDestinationMember>
        {
            throw new NotImplementedException();
        }
    }
}
