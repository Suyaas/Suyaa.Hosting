using Suyaa.Hosting.Common.DependencyManager.Dependency;

namespace Suyaa.Hosting.Common.Services
{
    /// <summary>
    /// 服务基础
    /// </summary>
    public abstract class ServiceBase
    {
        /// <summary>
        /// 获取服务供应商
        /// </summary>
        public IDependencyManager DependencyManager => sy.Hosting.DependencyManager;
    }
}
