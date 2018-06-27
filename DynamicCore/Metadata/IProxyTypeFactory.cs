using System;
using System.Collections.Generic;
using System.Text;

using System.Reflection;
using System.Reflection.Emit;

namespace Umi.Dynamic.Core.Metadata
{
    /// <summary>
    /// 代理类型生成工厂
    /// </summary>
    public interface IProxyTypeFactory
    {
        /// <summary>
        /// 定义泛型参数
        /// </summary>
        /// <param name="name">泛型参数名称</param>
        /// <returns></returns>
        GenericTypeParameterBuilder[] DefindGenericParameter(params string[] name);

        /// <summary>
        /// 设置其父类
        /// </summary>
        /// <param name="parent">父类类型</param>
        void SetParent(Type parent);

        /// <summary>
        /// 设置父类
        /// </summary>
        /// <typeparam name="T">父类类型</typeparam>
        void SetParent<T>();

        /// <summary>
        /// 设置实现的接口
        /// </summary>
        /// <param name="interface">接口类型</param>
        void AddInterface(Type @interface);

        /// <summary>
        /// 设置实现的接口
        /// </summary>
        /// <typeparam name="T">实现的接口</typeparam>
        void AddInterface<T>();

        /// <summary>
        /// 获取类型构建类型
        /// </summary>
        TypeBuilder TypeBuilder { get; }
    }
}
