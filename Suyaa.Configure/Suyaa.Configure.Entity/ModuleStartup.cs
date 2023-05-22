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

namespace Suyaa.Configure.Entities
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
            // 注册仓库
            services.AddTransient<IRepository<Project, string>, Repository<Project, string>>();
            // 注册数据库上下文
            services.AddScoped<ConfigDbContext>();
            // 注册实体
            services.AddTransient<Project>();
        }
    }
}
