﻿using Suyaa.DependencyInjection;
using Suyaa.Hosting.Kernel.Helpers;
using System.Reflection;
using Suyaa;
using Suyaa.Hosting.Kernel.WebApplicationProviders;
using Suyaa.DependencyInjection.ServiceCollection;
using Suyaa.Hosting.WebApplicationProviders;

namespace PagesDemo
{
    /// <summary>
    /// Demo应用供应商
    /// </summary>
    public sealed class DemoApplicationProvider : WebAppliactionProvider
    {
        /// <summary>
        /// 依赖管理器创建
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        protected override IDependencyManager OnDependencyManagerCreating(IServiceCollection services)
        {
            return new DependencyManager(services);
        }

        /// <summary>
        /// 配置依赖
        /// </summary>
        /// <param name="dependency"></param>
        protected override void OnConfigureDependency(IDependencyManager dependency)
        {
            base.OnConfigureDependency(dependency);
            // 添加模块注册
            //dependency.AddModuler<ModuleStartup>();
            // 注册切面
            dependency.AddActionFilters();
            // 添加EFCore支持
            //dependency.AddEFCore();
            // 注册对象映射
            //dependency.AddAutoMapper();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="builder"></param>
        public override void OnInitialize(WebApplicationBuilder builder)
        {
            string key = "ASPNETCORE_ENVIRONMENT";
            if (Environment.GetEnvironmentVariable(key).IsNullOrWhiteSpace()) Environment.SetEnvironmentVariable(key, "Production");
            builder.Configuration
                           .SetBasePath(Directory.GetCurrentDirectory())
                           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                           .AddEnvironmentVariables(prefix: "ASPNETCORE_");
            base.OnInitialize(builder);
        }

        protected override void OnConfigureAssembly(IList<Assembly> assemblies)
        {
            base.OnConfigureAssembly(assemblies);
            assemblies.Import<ModuleStartup>();
        }

    }
}
