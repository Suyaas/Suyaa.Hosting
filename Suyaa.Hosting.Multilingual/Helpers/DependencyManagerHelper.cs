﻿using Suyaa.DependencyInjection;
using Suyaa.Hosting.Configures;
using Suyaa.Hosting.Dependency;

namespace Suyaa.Hosting.Multilingual.Helpers
{
    /// <summary>
    /// 容器扩展
    /// </summary>
    public static partial class DependencyManagerHelper
    {
        /// <summary>
        /// 添加多语言支持
        /// </summary>
        /// <param name="dependency"></param>
        /// <returns></returns>
        public static IDependencyManager AddMultilingual(this IDependencyManager dependency)
        {
            // 注册多语言管理器
            dependency.Register<IMultilingualManager, MultilingualManager>();
            return dependency;
        }
    }
}
