using Suyaa.Hosting.Common.DependencyInjection;
using Suyaa.Hosting.Common.DependencyInjection.Dependency;
using Suyaa.Hosting.Common.Modules.Helpers;
using Suyaa.Hosting.Common.WebApplications;
using Suyaa.Hosting.Core.Helpers;

namespace Suyaa.Hosting.Core.WebApplications
{
    /// <summary>
    /// Web应用核心启动器
    /// </summary>
    public class CoreStartup : CommonStartup
    {
        /// <summary>
        /// 依赖配置
        /// </summary>
        /// <param name="dependencyManager"></param>
        protected override void OnConfigureDependency(IDependencyManager dependencyManager)
        {
            base.OnConfigureDependency(dependencyManager);
        }
    }
}
