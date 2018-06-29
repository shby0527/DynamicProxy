using System;
using System.Collections.Generic;
using System.Text;

namespace Umi.Dynamic.Core.Aspect
{
    /// <summary>
    /// AOP特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Module | AttributeTargets.Assembly | AttributeTargets.Struct, AllowMultiple = true, Inherited = true)]
    public abstract class AspectAttributeBase : Attribute, IAspect, IComparable, IComparable<IAspect>
    {
        /// <summary>
        /// 优先级
        /// </summary>
        public virtual int Priority { get; } = int.MaxValue;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            return obj == null ? 1 : (-Priority + ((IAspect)obj).Priority);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(IAspect other)
        {
            return other == null ? 1 : (-Priority + other.Priority);
        }

        /// <summary>
        /// 拦截器处理
        /// </summary>
        /// <param name="metadata">目标元数据</param>
        public abstract void Interceptor(AspectMetadata metadata);
    }
}
