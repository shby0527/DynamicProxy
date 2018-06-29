using System;
using System.Reflection;
using System.Reflection.Emit;

using Umi.Dynamic.Core;
using Umi.Dynamic.Core.Aspect;

namespace Umi.ConsoleTest
{
    [DefaultAspect]
    public interface IKk

    {
        int TestBB(int a, int b);
    }
    public class Program : IKk
    {

        public Program(int a, float b)
        {
            Console.WriteLine(a);
            Console.WriteLine(b);
        }

        public int TestBB(int a, int b)
        {
            throw new NotImplementedException();
        }
    }

    public class T
    {
        static void Main(string[] args)
        {
            IDynamicFactory factory = DynamicAssemblyBuilder.CreateDynamic("Test");
            var t = factory.CreateInterfaceProxy("!@#$%^&*(");
            Type ikkType = typeof(IKk);
            var gg = ikkType.GetGenericArguments();
            Type aspect = typeof(DefaultAspectAttribute);
            var c = new CustomAttributeBuilder(aspect.GetConstructors()[0], new object[0]);
            t.TypeFactory.TypeBuilder.SetCustomAttribute(c);
            //var gt = t.TypeFactory.DefindGenericParameter("T", "M", "N");
            //for (int i = 0; i < gg.Length; i++)
            //{
            //    var gpattr = gg[i].GenericParameterAttributes;
            //    if ((gpattr & GenericParameterAttributes.Contravariant) == GenericParameterAttributes.Contravariant)
            //        gpattr = gpattr ^ GenericParameterAttributes.Contravariant;
            //    if ((gpattr & GenericParameterAttributes.Covariant) == GenericParameterAttributes.Covariant)
            //        gpattr = gpattr ^ GenericParameterAttributes.Covariant;
            //    gt[i + 1].SetBaseTypeConstraint(gg[i].BaseType);
            //    gt[i + 1].SetInterfaceConstraints(gg[i].GetInterfaces());
            //    gt[i + 1].SetGenericParameterAttributes(gpattr);
            //}
            //var ikG = ikkType.MakeGenericType(gt[1], gt[2]);
            t.TypeFactory.AddInterface(ikkType);
            var field = t.BuildField("target", typeof(Program), FieldAttributes.Private);
            var type = t.BuildField("type", typeof(Type), FieldAttributes.Private);
            var proxyConstructor = t.ProxyConstructor(field, type);
            proxyConstructor.SetConstructor(null);
            var pm = t.ProxyMethod(field, ikkType.GetMethods()[0], type);
            pm.SetProxyMethod(ikkType.GetMethods()[0], ikkType);
            Type finish = t.Finish();
            var k = new Program(8, 9);

            IKk dist = (IKk)Activator.CreateInstance(finish, k, typeof(Program));
            dist.TestBB(1, 20);
        }
    }
}
