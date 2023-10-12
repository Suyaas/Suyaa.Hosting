using Suyaa.DependencyInjection;
using Suyaa.Hosting.Kernel.Attributes;
using Suyaa.Hosting.Kernel.Dependency;
using Suyaa.Hosting.Kernel.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: Module("demo")]
namespace SqlServerDemo
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
