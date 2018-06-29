using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Reflection.Emit;

using Umi.Dynamic.Core.Metadata;

namespace Umi.Dynamic.Core.Internel.Metadata
{
    internal class InterfaceConstructorProxyFactory : AbstractProxyBase, IProxyConstructorFactory
    {
        private ConstructorBuilder _builder;

        private readonly TypeBuilder _typeBuilder;

        public InterfaceConstructorProxyFactory(TypeBuilder builder, FieldInfo target, FieldInfo targetType)
            : base(target, targetType)
        {
            _typeBuilder = builder;
            _builder = null;
        }
        public void SetConstructor(ConstructorInfo constructor)
        {
            if (TargetField == null)
                throw new InvalidOperationException("target field is null");
            if (_builder != null)
                throw new InvalidOperationException("constructor is already generenced");
            Type fieldType = TargetField.FieldType;
            ParameterInfo[] paramInfos = null;
            if (constructor != null)
                paramInfos = constructor.GetParameters();
            else
                paramInfos = new ParameterInfo[0];
            Type[] parameterTypes = new Type[paramInfos.Length + 2];
            parameterTypes[0] = fieldType;
            parameterTypes[1] = typeof(Type);
            Array.Copy(paramInfos.Select(p => p.ParameterType).ToArray(), 0, parameterTypes, 2, paramInfos.Length);
            _builder = _typeBuilder.DefineConstructor(MethodAttributes.HideBySig | MethodAttributes.Public,
                                                     CallingConventions.Standard, parameterTypes);
            _builder.DefineParameter(1, ParameterAttributes.In, "target");
            _builder.DefineParameter(2, ParameterAttributes.In, "type");
            int i = 3;
            foreach (var item in paramInfos)
            {
                _builder.DefineParameter(i++, item.Attributes, item.Name);
            }
            ImplConstructor(constructor);
        }
        private void ImplConstructor(ConstructorInfo constructor)
        {
            #region the sample to generated IL code
            /*****************************************
             * ldarg.0
             * ldarg 2
             * ldarg 3
             * call instance void [Custom]Sample.Class::.ctor(string,int32)
             * ldarg.0
             * ldarg.1
             * stfld int32 Proxy.Proxy::target
             * ret
             * ***************************************/
            #endregion
            var generator = _builder.GetILGenerator();
            generator.Emit(OpCodes.Ldarg_0); // load this
            if (constructor != null)
            {
                // base(x,x,x)
                // expect first params  
                ParameterInfo[] parameters = constructor.GetParameters();
                for (int i = 0; i < parameters.Length; i++)
                {
                    generator.Emit(OpCodes.Ldarg, i + 3);// load other parameters,the first parameter is target object skip it
                }
                generator.Emit(OpCodes.Call, constructor); // and call the parent constructor
                // call the base constructor
            }
            else
            {
                // call object..ctor()
                var objConstructor = typeof(object).GetConstructor(new Type[0]);
                generator.Emit(OpCodes.Call, objConstructor);
                // base(); //object
            }
            generator.Emit(OpCodes.Ldarg_0);//load this pointer
            generator.Emit(OpCodes.Ldarg_1);//load the first parameter
            generator.Emit(OpCodes.Stfld, TargetField); // save the argument to the local field
            generator.Emit(OpCodes.Ldarg_0);//load this pointer
            generator.Emit(OpCodes.Ldarg_2);//load the first parameter
            generator.Emit(OpCodes.Stfld, TargetType); // save the argument to the local field
            generator.Emit(OpCodes.Ret);
            // finish the constructor and return
        }
    }
}
