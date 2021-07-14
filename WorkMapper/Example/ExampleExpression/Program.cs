using System;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;

namespace ExampleExpression
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = 0;
            MapFrom<Data, int>(x => x.Value);
            MapFrom<Data, int>(x => 0);
            MapFrom<Data, int>(x => a);
            MapFrom<Data, int>(x => x.Value.ToString().Length);
        }

        public static void MapFrom<TSource, TMember>(Expression<Func<TSource, TMember>> expression)
        {
            Debug.WriteLine("--");
            Debug.WriteLine(expression.NodeType);
            Debug.WriteLine(expression.Body.GetType());

            if (expression.Body is MemberExpression memberExpression)
            {
                var type = typeof(TSource);
                var pi = memberExpression.Member as PropertyInfo;
                if ((pi is null) || ((type != pi.ReflectedType) && !type.IsSubclassOf(pi.ReflectedType)))
                {
                    return;
                }

                Debug.WriteLine(pi.Name);
                return;
            }

            if (expression.Body is ConstantExpression constantExpression)
            {
                Debug.WriteLine(constantExpression.Value);
                return;
            }
        }

    }

    public class Data
    {
        public int Value { get; set; }
    }
}
