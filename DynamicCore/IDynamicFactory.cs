using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

using Umi.Dynamic.Core.Metadata;

namespace Umi.Dynamic.Core
{
    /// <summary>
    /// 生成代理工厂
    /// </summary>
    public interface IDynamicFactory
    {
        /// <summary>
        /// 创建类代理
        /// </summary>
        /// <param name="className">代理类名</param>
        /// <returns></returns>
        IProxyFactory CreateClassProxy(string className);

        /// <summary>
        /// 创建接口代理
        /// </summary>
        /// <param name="name">代理类名</param>
        /// <returns></returns>
        IProxyFactory CreateInterfaceProxy(string name);

        /// <summary>
        /// 最终程序集
        /// </summary>
        Assembly FinalAssembly { get; }
    }
}
