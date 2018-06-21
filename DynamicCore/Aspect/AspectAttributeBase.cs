using System;
using System.Collections.Generic;
using System.Text;

namespace Umi.Dynamic.Core.Aspect
{
    /// <summary>
    /// AOP特性
    /// </summary>
    public abstract class AspectAttributeBase : Attribute, IAspect
    {
        /// <summary>
        /// 优先级
        /// </summary>
        public virtual int Priority { get; } = 0;

        /// <summary>
        /// 拦截器处理
        /// </summary>
        /// <param name="metadata">目标元数据</param>
        public abstract void Interceptor(AspectMetadata metadata);
    }
}
