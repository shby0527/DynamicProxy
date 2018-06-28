using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;
using Umi.Dynamic.Core.Metadata;
using Umi.Dynamic.Core.Utils;

namespace Umi.Dynamic.Core.Internel
{
    internal class AssemblyProxyFactory : IDynamicFactory
    {
        private readonly AssemblyBuilder _builder;
        internal AssemblyProxyFactory(string name)
        {
            AssemblyName assemblyName = new AssemblyName(name)
            {
                Version = new Version(1, 0, 0, 0)
            };
            _builder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
        }

        public Assembly FinalAssembly => _builder;

        public IProxyFactory CreateClassProxy(string className)
        {
            throw new NotImplementedException();
        }

        public IProxyFactory CreateInterfaceProxy(string name)
        {
            ModuleBuilder moduleBuilder = _builder.DefineDynamicModule($"{ConstValue.MODULE_NAME_FOR_DYNAMIC}");
            return new InterfaceProxyFactory(moduleBuilder, name);
        }
    }
}
