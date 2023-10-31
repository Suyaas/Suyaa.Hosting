﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Suyaa.DependencyInjection;
using Suyaa.DependencyInjection.ServiceCollection;
using Suyaa.Hosting.Kernel.Helpers;
using Suyaa.Hosting.WebApplicationProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Suyaa.Hosting.AutoMapper.Helpers;
using Suyaa.Hosting.EFCore.Helpers;
using Microsoft.Extensions.Configuration;
using Suyaa;

namespace SqlServerDemo
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
            // 配置基础依赖
            base.OnConfigureDependency(dependency);
            // 添加模块注册
            dependency.AddModuler<ModuleStartup>();
            // 注册切片
            dependency.AddActionFilters();
            // 添加EFCore支持
            dependency.AddEFCore();
            // 注册对象映射
            dependency.AddAutoMapper();
        }

        /// <summary>
        /// 配置程序集
        /// </summary>
        /// <param name="assemblies"></param>
        protected override void OnConfigureAssembly(IList<Assembly> assemblies)
        {
            base.OnConfigureAssembly(assemblies);
            assemblies.Import<ModuleStartup>();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="builder"></param>
        public override void OnInitialize(WebApplicationBuilder builder)
        {
            base.OnInitialize(builder);
            string key = "ASPNETCORE_ENVIRONMENT";
            if (Environment.GetEnvironmentVariable(key).IsNullOrWhiteSpace()) Environment.SetEnvironmentVariable(key, "Production");
            builder.Configuration
                           .SetBasePath(Directory.GetCurrentDirectory())
                           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                           .AddEnvironmentVariables(prefix: "ASPNETCORE_");
        }

    }
}