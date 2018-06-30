using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Collections.Generic;
using Umi.Dynamic.Core;
using Umi.Dynamic.Core.Aspect;

namespace Umi.ConsoleTest
{
    public interface IKk
    {
        int TestBB(int a, int b);
    }

    public class MyTestAttribute : AspectAttributeBase
    {
        public override void Interceptor(AspectMetadata metadata)
        {
            foreach (var item in metadata.Parameters)
            {
                Console.WriteLine(item);
                Console.WriteLine("方法调用前");

            }
            metadata.Processed();
            Console.WriteLine("放回值");
            Console.WriteLine(metadata.Return);
            Console.WriteLine("调用后");
        }
    }

    public class Program : IKk
    {

        public Program(int a, float b)
        {
            Console.WriteLine(a);
            Console.WriteLine(b);
        }

        [MyTest]
        public int TestBB(int a, int b)
        {
            object x = a;

            int c = (int)x;

            return a + b;
        }
    }

    public class T
    {
        static void Main(string[] args)
        {
            IDynamicFactory factory = DynamicAssemblyBuilder.CreateDynamic("Test");
            var t = factory.CreateInterfaceProxy("test");
            Type ikkType = typeof(IKk);
            var gg = ikkType.GetGenericArguments();
            Type aspect = typeof(DefaultAspectAttribute);
            var c = new CustomAttributeBuilder(aspect.GetConstructors()[0], new object[0]);
            t.TypeFactory.TypeBuilder.SetCustomAttribute(c);
            t.TypeFactory.AddInterface(ikkType);
            var field = t.BuildField("target", typeof(IKk), FieldAttributes.Private);
            var type = t.BuildField("type", typeof(Type), FieldAttributes.Private);
            var proxyConstructor = t.ProxyConstructor(field, type);
            proxyConstructor.SetConstructor(null);
            var pm = t.ProxyMethod(field, ikkType.GetMethods()[0], type);
            pm.SetProxyMethod(ikkType.GetMethods()[0], ikkType);
            Type finish = t.Finish();
            var k = new Program(8, 9);
            IKk dist = (IKk)Activator.CreateInstance(finish, k, typeof(IKk));
            dist.TestBB(1, 20);
        }
    }
}
