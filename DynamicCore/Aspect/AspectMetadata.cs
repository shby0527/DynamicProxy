using System;
using System.Collections.Generic;
using System.Text;

using System.Reflection;

namespace Umi.Dynamic.Core.Aspect
{
    /// <summary>
    /// AOP元数据
    /// </summary>
    public sealed class AspectMetadata
    {
        internal AspectMetadata(Type[] arguments, object[] parameters, MethodInfo method, object target)
        {
            Arguments = arguments;
            Parameters = parameters;
            Target = target;
            Method = method;
        }

        /// <summary>
        /// 方法参数
        /// </summary>
        public object[] Parameters { get; }

        /// <summary>
        /// 参数类型
        /// </summary>
        public Type[] Arguments { get; }

        /// <summary>
        /// 返回值,返回值可以变更
        /// </summary>
        public object Return { get; set; }

        /// <summary>
        /// 返回值类型
        /// </summary>
        public Type ReturnType
        {
            get
            {
                return Method.ReturnType;
            }
        }

        /// <summary>
        /// 方法名
        /// </summary>
        public string MethodName
        {
            get
            {
                return Method.Name;
            }
        }

        /// <summary>
        /// 方法信息
        /// </summary>
        public MethodInfo Method { get; }

        /// <summary>
        /// 目标对象
        /// </summary>
        public object Target { get; }

        /// <summary>
        /// 调用方法
        /// </summary>
        /// <exception cref="InvalidOperationException">方法没有实现</exception>
        public void Processed()
        {
            if (Method.IsAbstract)
                throw new InvalidOperationException("抽象方法不可调用");

            Return = Method.Invoke(Target, Parameters);
        }
    }
}
