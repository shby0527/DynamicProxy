using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Umi.Dynamic.Core.Metadata
{
    /// <summary>
    /// 生成代理方法工厂
    /// </summary>
    public interface IProxyMethodFactory
    {
        /// <summary>
        /// 设置代理方法
        /// </summary>
        /// <param name="method">目标方法</param>
        void SetProxyMethod(MethodInfo method);
    }
}
