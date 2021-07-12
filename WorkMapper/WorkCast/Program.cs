using System;

namespace WorkCast
{
    class Program
    {
        static void Main(string[] args)
        {
            var ret1t = TypedConvert(ClassToClass, "");
            var ret2t = TypedConvert(ClassToStruct, "");
            var ret3t = TypedConvert(StructToStruct, 0);
            var ret4t = TypedConvert(StructToClass, 0);

            var ret1 = Convert(x => ClassToClass((string)x), "");
            var ret2 = Convert(x => ClassToStruct((string)x), "");
            var ret3 = Convert(x => StructToStruct((int)x), 0);
            var ret4 = Convert(x => StructToClass((int)x), 0);
        }

        private static TD TypedConvert<TS, TD>(Func<TS, TD> converter, TS source) => converter(source);

        private static object Convert(Func<object, object> converter, object source) => converter(source);

        private static string ClassToClass(string s) => s;

        private static int ClassToStruct(string s) => s.Length;

        private static int StructToStruct(int i) => i;

        private static string StructToClass(int i) => i.ToString();
    }
}
