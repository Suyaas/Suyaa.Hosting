using System.Reflection;
using Suyaa.Hosting.Common.Configures.Dependency;
using Suyaa.Hosting.Common.DependencyInjection;
using Suyaa.Hosting.Common.DependencyInjection.Dependency;

namespace Suyaa.Hosting.Common.Configures.Helpers
{
    /// <summary>
    /// 依赖管理器助手
    /// </summary>
    public static class DependencyManagerHelper
    {
        /// <summary>
        /// 注册可选配置
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IDependencyManager AddOptionConfigure(this IDependencyManager dependency)
        {
            dependency.Register(typeof(IOptionConfig<>), typeof(OptionConfig<>), Lifetimes.Transient);
            return dependency;
        }

        /// <summary>
        /// 注册配置相关的所有功能
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IDependencyManager AddConfigures(this IDependencyManager dependency)
        {
            dependency.AddOptionConfigure();
            return dependency;
        }
    }
}
