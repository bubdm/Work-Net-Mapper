namespace WorkMapper.Expressions
{
    using System;
    using System.Collections.Generic;

    internal class DefaultExpression : IDefaultExpression
    {
        //--------------------------------------------------------------------------------
        // Null
        //--------------------------------------------------------------------------------

        public IDefaultExpression NullIf<TMember>(TMember value)
        {
            throw new NotImplementedException();
        }

        public IDefaultExpression NullIf(IDictionary<Type, object> values)
        {
            throw new NotImplementedException();
        }

        //--------------------------------------------------------------------------------
        // Constant
        //--------------------------------------------------------------------------------

        public IDefaultExpression Const<TMember>(TMember value)
        {
            throw new NotImplementedException();
        }

        public IDefaultExpression Const(IDictionary<Type, object> values)
        {
            throw new NotImplementedException();
        }

        //--------------------------------------------------------------------------------
        // Convert
        //--------------------------------------------------------------------------------

        public IDefaultExpression ConvertUsing<TSourceMember, TDestinationMember>(IValueConverter<TSourceMember, TDestinationMember> converter)
        {
            throw new NotImplementedException();
        }

        public IDefaultExpression ConvertUsing<TSourceMember, TDestinationMember, TValueConverter>() where TValueConverter : IValueConverter<TSourceMember, TDestinationMember>
        {
            throw new NotImplementedException();
        }
    }
}
