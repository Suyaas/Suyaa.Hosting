using Suyaa.Hosting.Common.DependencyInjection.Dependency;

namespace Suyaa.Hosting.Common.Modules.Dependency
{
    /// <summary>
    /// 模块启动器
    /// </summary>
    public interface IModuleStartup
    {
        /// <summary>
        /// 配置依赖
        /// </summary>
        void ConfigureDependency(IDependencyManager dependency);
    }
}
