using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Reflection.Emit;

using Umi.Dynamic.Core.Metadata;

namespace Umi.Dynamic.Core.Internel.Metadata
{
    internal class ConstructorProxyFactory : IProxyConstructorFactory
    {
        private readonly ConstructorBuilder _builder;

        public ConstructorProxyFactory(ConstructorBuilder builder)
        {
            _builder = builder;
        }
        public void SetConstructor(ConstructorInfo constructor)
        {
            throw new NotImplementedException();
        }
    }
}
