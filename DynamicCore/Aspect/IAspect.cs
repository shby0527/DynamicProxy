using System;
using System.Collections.Generic;
using System.Text;

namespace Umi.Dynamic.Core.Aspect
{
    /// <summary>
    /// 处理拦截器
    /// </summary>
    public interface IAspect
    {
        /// <summary>
        /// 拦截器执行优先级
        /// </summary>
        int Priority { get; }

        /// <summary>
        /// 拦截器执行
        /// </summary>
        /// <param name="metadata">目标方法的元数据</param>
        object Interceptor(AspectMetadata metadata);
    }
}
