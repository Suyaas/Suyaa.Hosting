using Suyaa.Hosting.Common.Attributes;
using Suyaa.Hosting.Common.DependencyInjection.Dependency;
using Suyaa.Hosting.Common.Modules.Dependency;

[assembly: Module("demo")]
namespace PagesDemo
{
    /// <summary>
    /// 模块启动器
    /// </summary>
    public class ModuleStartup : IModuleStartup
    {
        public void ConfigureDependency(IDependencyManager dependency)
        {
            //dependency.AddModuler<ModuleStartup>();
            //throw new NotImplementedException();
        }
    }
}
