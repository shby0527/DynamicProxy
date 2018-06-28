using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Umi.Dynamic.Core.Metadata
{
    /// <summary>
    /// 设置属性代理
    /// </summary>
    public interface IProxyPropertyFactory : IProxyBase
    {
        /// <summary>
        /// 生成属性Read代理
        /// </summary>
        /// <param name="getter">被代理方法</param>
        void SetGetterProxy(MethodInfo getter);

        /// <summary>
        /// 设置属性Write代理
        /// </summary>
        /// <param name="setter">被代理方法</param>
        void SetSetterProxy(MethodInfo setter);
    }
}
