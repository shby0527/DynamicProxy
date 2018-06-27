using System;
using System.Collections.Generic;
using System.Text;

using Umi.Dynamic.Core.Internel;

namespace Umi.Dynamic.Core
{
    /// <summary>
    /// 动态程序集创建工厂
    /// </summary>
    public static class DynamicAssemblyBuilder
    {
        /// <summary>
        /// 创建代理程序集
        /// </summary>
        /// <param name="name">程序集名称</param>
        /// <returns>返回类型</returns>
        public static IDynamicFactory CreateDynamic(string name)
        {
            return new AssemblyProxyFactory(name);
        }
    }
}
