using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Umi.Dynamic.Core.Metadata
{
    /// <summary>
    /// 构造器代理工厂
    /// </summary>
    public interface IProxyConstructorFactory : IProxyBase
    {
        /// <summary>
        /// 设置代理目标构造器
        /// </summary>
        /// <param name="constructor">目标构造器</param>
        void SetConstructor(ConstructorInfo constructor);
    }
}
