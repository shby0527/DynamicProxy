﻿using System;
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
        /// <param name="name"></param>
        /// <returns></returns>
        IProxyPropertyFactory ProxyProperty(string name);

        /// <summary>
        /// 生成事件
        /// </summary>
        /// <param name="name">代理事件</param>
        /// <returns></returns>
        IProxyEventFactory ProxyEvent(string name);

        /// <summary>
        /// 生成方法
        /// </summary>
        /// <param name="name">方法名</param>
        /// <returns></returns>
        IProxyMethodFactory ProxyMethod(string name);

        /// <summary>
        /// 生成代理构造方法
        /// </summary>
        /// <returns></returns>
        IProxyConstructorFactory ProxyConstructor();

        /// <summary>
        /// 生成结束
        /// </summary>
        TypeInfo Finish();
    }
}