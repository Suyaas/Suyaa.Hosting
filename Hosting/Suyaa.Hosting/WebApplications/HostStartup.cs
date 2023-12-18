using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Suyaa.Hosting.App.FeatureProviders;
using Suyaa.Hosting.App.ModelConventions;
using Suyaa.Hosting.App.Options;
using Suyaa.Hosting.Common.ActionFilters;
using Suyaa.Hosting.Common.Configures;
using Suyaa.Hosting.Common.Configures.Dependency;
using Suyaa.Hosting.Common.Configures.Helpers;
using Suyaa.Hosting.Common.DependencyInjection;
using Suyaa.Hosting.Common.DependencyInjection.Dependency;
using Suyaa.Hosting.Common.DependencyInjection.Helpers;
using Suyaa.Hosting.Common.Resources;
using Suyaa.Hosting.Common.Sessions.Dependency;
using Suyaa.Hosting.Core.Helpers;
using Suyaa.Hosting.Infrastructure.Assemblies.Helpers;
using Suyaa.Hosting.Redirects.Helpers;
using Suyaa.Hosting.Sessions;
using Suyaa.Hosting.Sessions.Helpers;
using System.Reflection;

namespace Suyaa.Hosting.WebApplications
{
    /// <summary>
    /// Api应用供应商
    /// </summary>
    public class HostStartup : NeatStartup
    {
        // 私有变量定义
        private readonly List<Type> _filters;

        /// <summary>
        /// Api应用供应商
        /// </summary>
        public HostStartup()
        {
            _filters = new List<Type>();
        }

        /// <summary>
        /// 配置应用
        /// </summary>
        /// <param name="app"></param>
        protected override void OnConfigureApplication(WebApplication app)
        {
            base.OnConfigureApplication(app);

            #region 添加Swagger支持
            if (HostConfig.IsSwagger)
            {
                // 使用Swagger
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    foreach (var swagger in HostConfig.Swaggers)
                    {
                        options.SwaggerEndpoint($"/swagger/{swagger.Name}/swagger.json", swagger.Description);
                    }
                    //options.SwaggerEndpoint("/swagger/v1/swagger.json", "Suyaa Microservice API V1");
                    options.EnableFilter();
                });
            }
            #endregion

            #region 添加路由支持
            // 使用路由
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                // 映射所有页面
                if (HostConfig.IsRazorPageSupported) endpoints.MapRazorPages();
                // 映射所有控制器
                if (HostConfig.IsControllerSupported) endpoints.MapControllers();
            });
            #endregion

            // 添加重定向支持
            app.UseRedirects();
        }

        /// <summary>
        /// 配置依赖
        /// </summary>
        /// <param name="dependencyManager"></param>
        protected override void OnConfigureDependency(IDependencyManager dependencyManager)
        {
            base.OnConfigureDependency(dependencyManager);
            // 添加基础组件注入
            dependencyManager.Register<IHttpContextAccessor, HttpContextAccessor>(Lifetimes.Singleton);
            // 注入日志管理器
            dependencyManager.RegisterInstance<Logs.Dependency.ILogger>(sy.Logger.GetCurrentLogger());
            // 注入简单的交互信息模块
            dependencyManager.AddSession();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnConfigureBuilder(WebApplicationBuilder builder)
        {
            // 设置配置地址
            string configurePath = sy.Hosting.GetConfigurePath();
            sy.IO.CreateFolder(configurePath);
            builder.Configuration.SetBasePath(configurePath);

            base.OnConfigureBuilder(builder);

            // 添加重定向配置
            builder.AddRedirectConfigure();
        }

        /// <summary>
        /// 服务配置
        /// </summary>
        /// <param name="services"></param>
        protected override void OnConfigureServices(IServiceCollection services)
        {
            base.OnConfigureServices(services);

            // 输出服务注册日志
            sy.Logger.Debug($"Api services configure start ...", LogEvents.Services);

            #region 添加页面配置
            // 根据配置添加所有的控制器
            if (HostConfig.IsRazorPageSupported)
            {
                services.AddRazorPages(this.Assemblies);
            }
            #endregion

            #region 添加Swagger配置
            if (HostConfig.IsSwagger)
            {
                services.AddSwaggerGen(options =>
                {
                    foreach (var swagger in HostConfig.Swaggers)
                    {
                        options.SwaggerDoc(swagger.Name, new OpenApiInfo { Title = swagger.Description, Version = swagger.Name });
                    }
                    var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                    // 将所有xml文档添加到Swagger
                    var directory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
                    var files = directory.GetFiles("*.xml");
                    foreach (var file in files)
                    {
                        //if (swagger.Keyword == "*" || file.Name.Contains(swagger.Keyword))
                        options.IncludeXmlComments(file.FullName, true);
                    }
                    options.DocInclusionPredicate((docName, description) =>
                    {
                        var swagger = HostConfig.Swaggers.Where(d => d.Name == docName).FirstOrDefault();
                        if (swagger is null) return false;
                        if (swagger.Keyword == "*") return true;
                        if (description.RelativePath is null) return false;
                        if (description.ActionDescriptor is null) return false;
                        string? displayName = description.ActionDescriptor.DisplayName;
                        if (displayName is null) return false;
                        if (displayName.StartsWith(swagger.Keyword)) return true;
                        return false;
                    });
                });
            }
            #endregion

            #region 配置Razor页面
            if (HostConfig.IsRazorPageSupported) services.AddRazorPages();
            #endregion

            // 输出服务注册日志
            sy.Logger.Debug($"Api services configure completed.", LogEvents.Services);
        }

        /// <summary>
        /// 程序集配置
        /// </summary>
        /// <param name="assemblies"></param>
        protected override void OnConfigureAssembly(IList<Assembly> assemblies)
        {
            base.OnConfigureAssembly(assemblies);
            assemblies.Import<ModuleStartup>();
        }
    }
}
