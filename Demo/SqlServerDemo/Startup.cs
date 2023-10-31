using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Suyaa.DependencyInjection;
using Suyaa.DependencyInjection.ServiceCollection;
using Suyaa.Hosting;
using Suyaa.Hosting.AutoMapper.Helpers;
using Suyaa.Hosting.EFCore.Helpers;
using Suyaa.Hosting.Kernel.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlServerDemo
{
    /// <summary>
    /// 启动器
    /// </summary>
    public class Startup : HostStartupBase
    {
        /// <summary>
        /// 启动器
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration) : base(configuration)
        {
        }

        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        protected override void OnConfigure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// 配置依赖
        /// </summary>
        /// <param name="dependency"></param>
        protected override void OnConfigureDependency(IDependencyManager dependency)
        {
            // 添加模块注册
            dependency.AddModuler<ModuleStartup>();
            // 注册切片
            dependency.AddActionFilters();
            // 添加EFCore支持
            dependency.AddEFCore();
            // 注册对象映射
            dependency.AddAutoMapper();
            //throw new NotImplementedException();
        }

        /// <summary>
        /// 创建依赖
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        protected override IDependencyManager OnDependencyManagerCreating(IServiceCollection services)
        {
            //throw new NotImplementedException();
            return new DependencyManager(services);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        protected override void OnInitialize()
        {
            this.Import<ModuleStartup>();
            //throw new NotImplementedException();
        }
    }
}
