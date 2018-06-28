using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

using Umi.Dynamic.Core.Metadata;

namespace Umi.Dynamic.Core.Internel.Metadata
{
    internal class MethodProxyFactory : AbstractProxyBase, IProxyMethodFactory
    {
        private readonly TypeBuilder _typeBuilder;
        private MethodBuilder _builder;
        private readonly MethodInfo _overrided;
        public MethodProxyFactory(FieldInfo field, TypeBuilder typeBuilder, MethodInfo overriderMethod)
            : base(field)
        {
            _builder = null;
            _typeBuilder = typeBuilder;
            _overrided = overriderMethod;
        }
        public void SetProxyMethod(MethodInfo method)
        {
            if (_builder != null)
                throw new InvalidOperationException("method already generenced");
            Type rtnType = method.ReturnType;
            ParameterInfo[] parameters = method.GetParameters();
            _builder = _typeBuilder.DefineMethod(method.Name, method.Attributes ^ MethodAttributes.Abstract,
                CallingConventions.Standard, rtnType, parameters.Select(p => p.ParameterType).ToArray());
            if (_overrided != null)
                _typeBuilder.DefineMethodOverride(_builder, _overrided); // override the method
            ImplMethod(method);
        }
        private void ImplMethod(MethodInfo method)
        {
            ILGenerator generator = _builder.GetILGenerator();
            ParameterInfo[] parameters = method.GetParameters();
            // 
        }
    }
}
