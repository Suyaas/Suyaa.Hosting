using Suyaa.Logs;
using Suyaa.Logs.Loggers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Suyaa.Apis.Dependency;
using Suyaa.Hosting;
using Suyaa.Data;
using Suyaa.Configure.Entity.Projects;
using Suyaa.Hosting.Helpers;

namespace Suyaa.Configure.Apps
{
    public class ModuleStartup : IModuleStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // 注册仓库
            services.AddModuler<Cores.ModuleStartup>();
        }
    }
}
