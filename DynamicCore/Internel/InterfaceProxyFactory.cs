using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using Umi.Dynamic.Core.Internel.Metadata;
using Umi.Dynamic.Core.Metadata;
using Umi.Dynamic.Core.Utils;

namespace Umi.Dynamic.Core.Internel
{
    internal class InterfaceProxyFactory : IProxyFactory
    {
        private readonly TypeBuilder _typeBuilder;

        private bool isFinish = false;

        internal InterfaceProxyFactory(ModuleBuilder builder, string className)
        {
            _typeBuilder = builder.DefineType($"{ConstValue.NAMESPACE_FOR_DYNAMIC}.{className}");
        }

        public IProxyTypeFactory TypeFactory => new ProxyTypeFactory(_typeBuilder);

        public TypeInfo Finish()
        {
            if (isFinish)
                throw new InvalidOperationException("Already finished");
            isFinish = true;
            return _typeBuilder.CreateTypeInfo();
        }

        public IProxyConstructorFactory ProxyConstructor()
        {
            if (isFinish)
                throw new InvalidOperationException("Already finished");
            throw new NotSupportedException("interface proxy not support constructor proxy");
        }

        public IProxyEventFactory ProxyEvent(string name)
        {
            if (isFinish)
                throw new InvalidOperationException("Already finished");
            throw new NotImplementedException();
        }

        public IProxyMethodFactory ProxyMethod(string name)
        {
            if (isFinish)
                throw new InvalidOperationException("Already finished");
            throw new NotImplementedException();
        }

        public IProxyPropertyFactory ProxyProperty(string name)
        {
            if (isFinish)
                throw new InvalidOperationException("Already finished");
            throw new NotImplementedException();
        }
    }
}
