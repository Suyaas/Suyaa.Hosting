using Suyaa.Hosting.Common.ActionFilters.Dependency;
using Suyaa.Hosting.Common.DependencyManager.Dependency;
using Suyaa.Hosting.Common.DependencyManager.Helpers;

namespace Suyaa.Hosting.Kernel.Helpers
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
            dependency.RegisterTransients<IActionFilterProvider>();
            return dependency;
        }
    }
}
