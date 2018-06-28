using System;

using Umi.Dynamic.Core;

namespace Umi.ConsoleTest
{
    public class Program
    {

        public Program(int a, float b)
        {
            Console.WriteLine(a);
            Console.WriteLine(b);
        }
        static void Main(string[] args)
        {
            IDynamicFactory factory = DynamicAssemblyBuilder.CreateDynamic("Test");
            var t = factory.CreateInterfaceProxy("Test");
            t.TypeFactory.SetParent<Program>();
            var field = t.BuildField("target", typeof(Program), System.Reflection.FieldAttributes.Private);
            var proxyConstructor = t.ProxyConstructor(field);
            proxyConstructor.SetConstructor(typeof(Program).GetConstructor(new Type[] { typeof(int), typeof(float) }));
            Type finish = t.Finish();
            var k = new Program(8, 9);
            Program dist = (Program)Activator.CreateInstance(finish, k, 8, 9);
        }
    }
}
