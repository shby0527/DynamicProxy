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

        public FieldInfo BuildField(string name, Type fieldType, FieldAttributes attributes)
        {
            if (isFinish)
                throw new InvalidOperationException("Already finished");
            FieldBuilder fieldBuilder = _typeBuilder.DefineField(name, fieldType, attributes);
            return fieldBuilder;
        }

        public TypeInfo Finish()
        {
            if (isFinish)
                throw new InvalidOperationException("Already finished");
            isFinish = true;
            return _typeBuilder.CreateTypeInfo();
        }

        public IProxyConstructorFactory ProxyConstructor(FieldInfo target)
        {
            if (isFinish)
                throw new InvalidOperationException("Already finished");
            return new ConstructorProxyFactory(_typeBuilder, target);
        }

        public IProxyEventFactory ProxyEvent(FieldInfo target, EventInfo overrider)
        {
            if (isFinish)
                throw new InvalidOperationException("Already finished");
            throw new NotImplementedException();
        }

        public IProxyMethodFactory ProxyMethod(FieldInfo target, MethodInfo overrider)
        {
            if (isFinish)
                throw new InvalidOperationException("Already finished");
            return new MethodProxyFactory(target, _typeBuilder, overrider);
        }

        public IProxyPropertyFactory ProxyProperty(FieldInfo target, PropertyInfo overrider)
        {
            if (isFinish)
                throw new InvalidOperationException("Already finished");
            throw new NotImplementedException();
        }
    }
}
