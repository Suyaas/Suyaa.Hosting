using Suyaa.Logs;
using Suyaa.Logs.Loggers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Suyaa.Data;
using Suyaa.Hosting.Helpers;
using Suyaa.Configure.Basic.Dependency;
using Suyaa.Hosting.Kernel.Dependency;
using Suyaa.DependencyInjection;
using Suyaa.Hosting.Kernel.Helpers;

namespace Suyaa.Configure.Basic
{
    /// <summary>
    /// 模块启动器
    /// </summary>
    public class ModuleStartup : IModuleStartup
    {
        public void ConfigureDependency(IDependencyManager dependency)
        {
            // 注入模块标准对象
            dependency.AddModulerIoc<ModuleStartup>();
        }

        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {

        }
    }
}
