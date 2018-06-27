using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

using Umi.Dynamic.Core.Metadata;

namespace Umi.Dynamic.Core.Internel.Metadata
{
    internal class ProxyTypeFactory : IProxyTypeFactory
    {
        internal ProxyTypeFactory(TypeBuilder builder)
        {
            TypeBuilder = builder;
        }

        public TypeBuilder TypeBuilder { get; }

        public void AddInterface(Type @interface)
        {
            if (!@interface.IsInterface)
                throw new InvalidOperationException("not a interface type");
            TypeBuilder.AddInterfaceImplementation(@interface);
        }

        public void AddInterface<T>()
        {
            AddInterface(typeof(T));
        }

        public GenericTypeParameterBuilder[] DefindGenericParameter(params string[] name)
        {
            return TypeBuilder.DefineGenericParameters(name);
        }

        public void SetParent(Type parent)
        {
            if (parent.IsInterface)
                throw new InvalidOperationException("type not be a interface");
            TypeBuilder.SetParent(parent);
        }

        public void SetParent<T>()
        {
            TypeBuilder.SetParent(typeof(T));
        }
    }
}
