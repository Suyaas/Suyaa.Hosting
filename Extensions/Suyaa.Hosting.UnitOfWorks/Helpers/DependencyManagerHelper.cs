using Suyaa.Hosting.Common.DependencyManager.Dependency;

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
        public static IDependencyManager AddUnitOfWork(this IDependencyManager dependency)
        {
            return dependency;
        }
    }
}
