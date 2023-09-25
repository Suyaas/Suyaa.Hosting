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
using Suyaa.EFCore;
using Suyaa.EFCore.Dependency;
using Suyaa.EFCore.Dbsets;
using Suyaa.Hosting.Kernel.Dependency;
using Suyaa.DependencyInjection;

namespace Suyaa.Configure.Entities
{
    /// <summary>
    /// 模块启动器
    /// </summary>
    public class ModuleStartup : IModuleStartup
    {
        /// <summary>
        /// 依赖初始化
        /// </summary>
        /// <param name="dependency"></param>
        public void ConfigureDependency(IDependencyManager dependency)
        {

        }

        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            //// 注册仓库
            //services.AddTransient<IRepository<Project, string>, Repository<Project, string>>();
            //// 注册数据库上下文
            //services.AddScoped<DbContextBase, ConfigDbContext>();
            //// 注册实体
            //services.AddTransient<Project>();
        }
    }
}
