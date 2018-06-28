using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Umi.Dynamic.Core.Metadata;

namespace Umi.Dynamic.Core.Internel.Metadata
{
    /// <summary>
    /// 抽象基类
    /// </summary>
    internal abstract class AbstractProxyBase : IProxyBase
    {

        protected AbstractProxyBase(FieldInfo fieldInfo)
        {
            TargetField = fieldInfo;
        }

        /// <summary>
        /// 获取或设置代理目标
        /// </summary>
        public FieldInfo TargetField { get; }
    }
}
