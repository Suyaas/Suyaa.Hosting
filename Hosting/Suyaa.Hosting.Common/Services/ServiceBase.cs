using Suyaa.Hosting.Common.DependencyInjection.Dependency;
using Suyaa.Hosting.Common.DependencyInjection;

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
        public IDependencyManager DependencyManager => DependencyInjection.DependencyManager.GetCurrent() ?? throw new NullException<IDependencyManager>();
    }
}
