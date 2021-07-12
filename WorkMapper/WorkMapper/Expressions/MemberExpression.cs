//namespace WorkMapper.Expressions
//{
//    using System;
//    using System.Linq.Expressions;
//    using System.Reflection;

//    internal class MemberExpression<TSource, TDestination, TMember> : IMemberExpression<TSource, TDestination, TMember>
//    {
//        public MemberInfo DestinationMember { get; }

//        public MemberExpression(MemberInfo destinationMember)
//        {
//            DestinationMember = destinationMember;
//        }

//        public void Ignore()
//        {
//            throw new NotImplementedException();
//        }

//        public void Order(int order)
//        {
//            throw new NotImplementedException();
//        }

//        public void Condition(Func<TSource, bool> condition)
//        {
//            throw new NotImplementedException();
//        }

//        public void Condition(Func<TSource, object, bool> condition)
//        {
//            throw new NotImplementedException();
//        }

//        public void Condition(Func<TSource, TDestination, bool> condition)
//        {
//            throw new NotImplementedException();
//        }

//        public void Condition(Func<TSource, TDestination, object, bool> condition)
//        {
//            throw new NotImplementedException();
//        }

//        public void MapFrom<TSourceMember>(Expression<Func<TSource, TSourceMember>> expression)
//        {
//            throw new NotImplementedException();
//        }

//        public void MapFrom<TSourceMember>(Expression<Func<TSource, object, TSourceMember>> expression)
//        {
//            throw new NotImplementedException();
//        }

//        public void MapFrom(IValueResolver<TSource, TDestination, TMember> resolver)
//        {
//            throw new NotImplementedException();
//        }

//        public void MapFrom<TValueResolver>() where TValueResolver : IValueResolver<TSource, TDestination, TMember>
//        {
//            throw new NotImplementedException();
//        }

//        public void MapFrom(string name)
//        {
//            throw new NotImplementedException();
//        }

//        public void NullIf(TMember value)
//        {
//            throw new NotImplementedException();
//        }

//        public void Const(TMember value)
//        {
//            throw new NotImplementedException();
//        }

//        public void ConvertUsing<TSourceMember>(IValueConverter<TSourceMember, TMember> converter)
//        {
//            throw new NotImplementedException();
//        }

//        public void ConvertUsing<TSourceMember, TValueConverter>() where TValueConverter : IValueConverter<TSourceMember, TMember>
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
