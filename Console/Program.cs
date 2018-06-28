using System;

using Umi.Dynamic.Core;

namespace Umi.ConsoleTest
{
    public class Program<T>
    {

        public Program(T a, float b)
        {
            Console.WriteLine(a);
            Console.WriteLine(b);
        }
    }

    public class T
    {
        static void Main(string[] args)
        {
            IDynamicFactory factory = DynamicAssemblyBuilder.CreateDynamic("Test");
            var t = factory.CreateInterfaceProxy("!@#$%^&*(");
            t.TypeFactory.SetParent(typeof(Program<>));
            var gt = t.TypeFactory.DefindGenericParameter("T");
            var field = t.BuildField("target", typeof(Program<>), System.Reflection.FieldAttributes.Private);
            var proxyConstructor = t.ProxyConstructor(field);
            proxyConstructor.SetConstructor(typeof(Program<>).GetConstructors()[0]);
            Type finish = t.Finish();
            var k = new Program<float>(8, 9);
            finish = finish.MakeGenericType(typeof(float));
            object dist = Activator.CreateInstance(finish, k, (float)8, 9);
        }
    }
}
