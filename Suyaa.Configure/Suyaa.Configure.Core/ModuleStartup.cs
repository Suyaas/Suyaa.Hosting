using Suyaa.Logs;
using Suyaa.Logs.Loggers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Suyaa.Hosting;
using Suyaa.Data;
using Suyaa.Configure.Entity.Projects;
using Suyaa.Hosting.Helpers;

namespace Suyaa.Configure.Cores
{
    /// <summary>
    /// 模块启动器
    /// </summary>
    public class ModuleStartup : IModuleStartup
    {
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddModulerIoc<ModuleStartup>();
            services.AddModuler<Entities.ModuleStartup>();
        }
    }
}
