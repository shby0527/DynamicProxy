using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Umi.Dynamic.Core.Metadata
{
    /// <summary>
    /// 代理设置
    /// </summary>
    public interface IProxyFactory
    {

        /// <summary>
        /// 获取代理类型生成工厂
        /// </summary>
        IProxyTypeFactory TypeFactory { get; }

        /// <summary>
        /// 生成属性代理
        /// </summary>
        /// <param name="target">目标对象的字段</param>
        /// <param name="overrider">被重写的属性</param>
        /// <returns></returns>
        IProxyPropertyFactory ProxyProperty(FieldInfo target, PropertyInfo overrider);

        /// <summary>
        /// 生成事件
        /// </summary>
        /// <param name="target">目标对象的字段</param>
        /// <param name="overrider">被重写的事件</param>
        /// <returns></returns>
        IProxyEventFactory ProxyEvent(FieldInfo target, EventInfo overrider);

        /// <summary>
        /// 生成方法
        /// </summary>
        /// <param name="target">目标对象的字段</param>
        /// <param name="overrider">被重写的方法</param>
        /// <returns></returns>
        IProxyMethodFactory ProxyMethod(FieldInfo target, MethodInfo overrider);

        /// <summary>
        /// 生成代理构造方法
        /// </summary>
        /// <param name="target">目标对象的字段</param>
        /// <returns></returns>
        IProxyConstructorFactory ProxyConstructor(FieldInfo target);

        /// <summary>
        /// 创建字段
        /// </summary>
        /// <param name="name">字段名</param>
        /// <param name="fieldType">字段类型</param>
        /// <param name="attributes">字段属性</param>
        /// <returns>返回字段信息</returns>
        FieldInfo BuildField(string name, Type fieldType, FieldAttributes attributes);

        /// <summary>
        /// 生成结束
        /// </summary>
        TypeInfo Finish();
    }
}
