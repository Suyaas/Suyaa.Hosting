using Suyaa.Hosting.Common.ActionFilters.Dependency;
using Suyaa.Hosting.Common.DependencyInjection;
using Suyaa.Hosting.Common.DependencyInjection.Dependency;
using Suyaa.Hosting.Common.DependencyInjection.Helpers;

namespace Suyaa.Hosting.Common.ActionFilters.Helpers
{
    /// <summary>
    /// 容器扩展
    /// </summary>
    public static partial class DependencyManagerHelper
    {
        /// <summary>
        /// 添加切片支持
        /// </summary>
        /// <param name="dependency"></param>
        /// <returns></returns>
        public static IDependencyManager AddActionFilters(this IDependencyManager dependency)
        {
            dependency.RegisterTransientImplementations<IActionFilterProvider>();
            dependency.Register<IApiExecutedProvider, ApiExecutedProvider>(Lifetimes.Transient);
            return dependency;
        }
    }
}
