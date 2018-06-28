using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Umi.Dynamic.Core.Metadata
{
    /// <summary>
    /// 代理基础接口
    /// </summary>
    public interface IProxyBase
    {
        /// <summary>
        /// 获取或设置代理目标
        /// </summary>
        FieldInfo TargetField { get; }
    }
}
