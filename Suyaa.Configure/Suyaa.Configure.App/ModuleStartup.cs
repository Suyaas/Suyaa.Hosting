using Suyaa.Logs;
using Suyaa.Logs.Loggers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Suyaa.Data;
using Suyaa.Configure.Entity.Projects;
using Suyaa.Hosting.Helpers;
using Suyaa.Hosting.Kernel.Dependency;
using Suyaa.DependencyInjection;
using Suyaa.Hosting.Kernel.Helpers;

namespace Suyaa.Configure.Apps
{
    /// <summary>
    /// 模块启动器
    /// </summary>
    public class ModuleStartup : IModuleStartup
    {
        /// <summary>
        /// 配置依赖
        /// </summary>
        /// <param name="dependency"></param>
        public void ConfigureDependency(IDependencyManager dependency)
        {
            dependency.AddModuler<Basic.ModuleStartup>().AddModuler<Cores.ModuleStartup>();
        }
    }
}
