using Suyaa.DependencyInjection;
using Suyaa.Hosting.Kernel.Dependency;
using System.Reflection;

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
