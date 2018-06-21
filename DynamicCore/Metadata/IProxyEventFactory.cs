using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Umi.Dynamic.Core.Metadata
{
    /// <summary>
    /// 生成事件代理
    /// </summary>
    public interface IProxyEventFactory
    {
        /// <summary>
        /// 设置Add方法代理
        /// </summary>
        /// <param name="method"></param>
        void SetAddProxy(MethodInfo method);

        /// <summary>
        /// 设置Remove方法代理
        /// </summary>
        /// <param name="method"></param>
        void SetRemoveProxy(MethodInfo method);
    }
}
