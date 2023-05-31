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
using Suyaa.Hosting.Helpers;
using Suyaa.Configure.Basic.Dependency;
using Suyaa.Configure.Basic.MetaDatas;

namespace Suyaa.Configure.Basic
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
            // 注入模块标准对象
            services.AddModulerIoc<ModuleStartup>();
            // 注册应用授权数据
            services.AddScoped<AppAuthorizeMetaData>();
        }
    }
}
