using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using Umi.Dynamic.Core.Aspect;

namespace Umi.Dynamic.Core.Utils
{
    /// <summary>
    /// 方法工具包
    /// </summary>
    public static class MethodUtils
    {
        /// <summary>
        /// 创建AOP元数据
        /// </summary>
        /// <param name="method">方法信息</param>
        /// <param name="target">代理目标</param>
        /// <param name="parameters">参数类型</param>
        /// <param name="arguments">参数值</param>
        /// <returns></returns>
        public static AspectMetadata CreateAspectMetadata(MethodInfo method, object target,
            Type[] parameters, object[] arguments)
        {
            return new AspectMetadata(parameters, arguments, method, target);
        }

        /// <summary>
        /// 全局拦截器
        /// </summary>
        public static IEnumerable<IAspect> GlobleInterceptors { get; set; }

        /// <summary>
        /// 尝试调用
        /// </summary>
        /// <param name="targetType"></param>
        /// <param name="methodName">调用的方法名</param>
        /// <param name="target"></param>
        /// <param name="arguments"></param>
        public static void CallTarget(Type targetType, string methodName, object target, object[] arguments)
        {
            List<IAspect> aspects = new List<IAspect>();
            if (GlobleInterceptors != null)
                aspects.AddRange(GlobleInterceptors);
            var customAttributes = targetType.Assembly.GetCustomAttributes(true);
            aspects.AddRange(customAttributes.Where(p => p is IAspect).Select(p => p as IAspect));
            customAttributes = targetType.Module.GetCustomAttributes(true);
            aspects.AddRange(customAttributes.Where(p => p is IAspect).Select(p => p as IAspect));
            customAttributes = targetType.GetCustomAttributes(true);
            aspects.AddRange(customAttributes.Where(p => p is IAspect).Select(p => p as IAspect));
            aspects.Sort();
            Type type = target.GetType();
            Type[] parameters = arguments.Select(p => p.GetType()).ToArray();
            var methodInfo = type.GetMethod(methodName, parameters);
            if (methodInfo == null) //public not found?
                methodInfo = type.GetMethod(methodName, BindingFlags.Instance | BindingFlags.NonPublic, null, parameters, null);
            if (methodInfo == null)
                throw new InvalidOperationException("method not found");
            var aspectMetadata = CreateAspectMetadata(methodInfo, target, parameters, arguments);
            CallInterceptorChina(aspects, aspectMetadata);
        }
        private static void CallInterceptorChina(IEnumerable<IAspect> aspects, AspectMetadata lastest)
        {
            var enumerator = aspects.GetEnumerator();
            AspectMetadata metadata = lastest;
            IAspect first = !enumerator.MoveNext() ? new DefaultAspectAttribute() : enumerator.Current;
            while (enumerator.MoveNext())
            {
                IAspect current = enumerator.Current;
                Type aspectType = current.GetType();
                Type[] types = new Type[] { typeof(AspectMetadata) };
                MethodInfo method = aspectType.GetMethod("Interceptor", types);
                metadata = CreateAspectMetadata(method, current, types, new object[] { metadata });
            }
            first.Interceptor(metadata);
        }
    }
}
