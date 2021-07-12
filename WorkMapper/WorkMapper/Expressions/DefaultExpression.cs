//namespace WorkMapper.Expressions
//{
//    using System;
//    using System.Collections.Generic;

//    using WorkMapper.Metadata;

//    internal class DefaultExpression : IDefaultExpression
//    {
//        private readonly DefaultEntry entry;

//        public DefaultExpression(DefaultEntry entry)
//        {
//            this.entry = entry;
//        }

//        //--------------------------------------------------------------------------------
//        // Null
//        //--------------------------------------------------------------------------------

//        public IDefaultExpression NullIf<TMember>(TMember value)
//        {
//            entry.SetNullIfValue(typeof(TMember), value);
//            return this;
//        }

//        public IDefaultExpression NullIf(IDictionary<Type, object> values)
//        {
//            foreach (var pair in values)
//            {
//                entry.SetNullIfValue(pair.Key, pair.Value);
//            }

//            return this;
//        }

//        //--------------------------------------------------------------------------------
//        // Constant
//        //--------------------------------------------------------------------------------

//        public IDefaultExpression Const<TMember>(TMember value)
//        {
//            entry.SetConstValue(typeof(TMember), value);
//            return this;
//        }

//        public IDefaultExpression Const(IDictionary<Type, object> values)
//        {
//            foreach (var pair in values)
//            {
//                entry.SetConstValue(pair.Key, pair.Value);
//            }

//            return this;
//        }

//        //--------------------------------------------------------------------------------
//        // Converter
//        //--------------------------------------------------------------------------------

//        public IDefaultExpression ConvertUsing<TSourceMember, TDestinationMember>(IValueConverter<TSourceMember, TDestinationMember> converter)
//        {
//            entry.SetConverter(new Tuple<Type, Type>(typeof(TSourceMember), typeof(TDestinationMember)), converter);
//            return this;
//        }

//        public IDefaultExpression ConvertUsing<TSourceMember, TDestinationMember, TValueConverter>() where TValueConverter : IValueConverter<TSourceMember, TDestinationMember>
//        {
//            entry.SetConverterType(new Tuple<Type, Type>(typeof(TSourceMember), typeof(TDestinationMember)), typeof(TValueConverter));
//            return this;
//        }
//    }
//}
