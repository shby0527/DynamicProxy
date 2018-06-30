using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

using Umi.Dynamic.Core.Metadata;
using Umi.Dynamic.Core.Utils;

namespace Umi.Dynamic.Core.Internel.Metadata
{
    internal class MethodProxyFactory : AbstractProxyBase, IProxyMethodFactory
    {
        private readonly TypeBuilder _typeBuilder;
        private MethodBuilder _builder;
        private readonly MethodInfo _overrided;
        public MethodProxyFactory(FieldInfo field, TypeBuilder typeBuilder, MethodInfo overriderMethod, FieldInfo targetType)
            : base(field, targetType)
        {
            _builder = null;
            _typeBuilder = typeBuilder;
            _overrided = overriderMethod;
        }
        public void SetProxyMethod(MethodInfo method, Type declaredType)
        {
            if (_builder != null)
                throw new InvalidOperationException("method already generenced");
            ParameterInfo[] parameters = method.GetParameters();
            MethodAttributes methodAttributes = method.Attributes;
            if ((methodAttributes & MethodAttributes.Abstract) == MethodAttributes.Abstract)
                methodAttributes = methodAttributes ^ MethodAttributes.Abstract;
            var mparam = parameters.Select(p => p.ParameterType).ToArray();
            Type rtnType = method.ReturnType;
            _builder = _typeBuilder.DefineMethod(method.Name, methodAttributes,
                method.CallingConvention, rtnType, mparam);
            var genericTypes = method.GetGenericArguments();
            if (genericTypes != null && genericTypes.Length > 0)
            {
                var gbuild = _builder.DefineGenericParameters(genericTypes.Select(p => p.Name).ToArray());
                for (int i = 0; i < gbuild.Length; i++)
                {
                    gbuild[i].SetBaseTypeConstraint(genericTypes[i].BaseType);
                    gbuild[i].SetInterfaceConstraints(genericTypes[i].GetInterfaces());
                    gbuild[i].SetGenericParameterAttributes(genericTypes[i].GenericParameterAttributes);
                }
            }
            ImplMethod(method);
            if (_overrided != null)
                _typeBuilder.DefineMethodOverride(_builder, _overrided); // override the method
                                                                         // _builder.SetImplementationFlags(MethodImplAttributes.IL);
        }
        private void ImplMethod(MethodInfo method)
        {
            ILGenerator generator = _builder.GetILGenerator();
            ParameterInfo[] parameters = method.GetParameters();
            Type methodUtils = typeof(MethodUtils);
            MethodInfo callTarget = methodUtils.GetMethod("CallTarget", new Type[] { typeof(Type), typeof(string),
                typeof(object), typeof(object[]) });
            #region C# 代码
            /************************************************
             * object[] params = new object[]{arg1,arg2,arg3,arg4……};
             * MethodUtils.CallTarget(targetType,"methodName",target,params)
             * **********************************************/
            #endregion
            generator.DeclareLocal(typeof(object[]));
            generator.Emit(OpCodes.Ldc_I4_S, parameters.Length);
            generator.Emit(OpCodes.Newarr, typeof(object));
            for (int i = 0; i < parameters.Length; i++)
            {
                generator.Emit(OpCodes.Dup);
                generator.Emit(OpCodes.Ldc_I4_S, i);
                generator.Emit(OpCodes.Ldarg_S, i + 1);
                if (parameters[i].ParameterType.IsGenericType || parameters[i].ParameterType.IsValueType)
                    generator.Emit(OpCodes.Box, parameters[i].ParameterType);
                generator.Emit(OpCodes.Stelem_Ref);
            }
            generator.Emit(OpCodes.Stloc_0);
            generator.Emit(OpCodes.Ldarg_0);
            generator.Emit(OpCodes.Ldfld, TargetType);
            generator.Emit(OpCodes.Ldstr, method.Name);
            generator.Emit(OpCodes.Ldarg_0);
            generator.Emit(OpCodes.Ldfld, TargetField);
            generator.Emit(OpCodes.Ldloc_0);
            generator.Emit(OpCodes.Call, callTarget);
            if (method.ReturnType.IsGenericParameter || method.ReturnType.IsValueType)
                generator.Emit(OpCodes.Unbox_Any, method.ReturnType);
            if (method.ReturnType == typeof(void))
                generator.Emit(OpCodes.Pop);
            generator.Emit(OpCodes.Ret);
        }
    }
}
