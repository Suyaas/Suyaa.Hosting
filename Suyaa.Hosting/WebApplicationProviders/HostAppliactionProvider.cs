using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Suyaa.DependencyInjection;
using Suyaa.Hosting.Configures;
using Suyaa.Hosting.Kernel;
using Suyaa.Hosting.Kernel.ActionFilters;
using Suyaa.Hosting.Kernel.ApplicationModelConventions;
using Suyaa.Hosting.Kernel.Configures;
using Suyaa.Hosting.Kernel.Dependency;
using Suyaa.Hosting.Kernel.FeatureProviders;
using Suyaa.Hosting.Kernel.Helpers;
using Suyaa.Hosting.Kernel.WebApplicationProviders;
using Suyaa.Hosting.Options;
using System.Reflection;

namespace Suyaa.Hosting.WebApplicationProviders
{
    /// <summary>
    /// Api应用供应商
    /// </summary>
    public abstract class HostAppliactionProvider : NeatApplicationProvider
    {
        // 私有变量定义
        private readonly List<Assembly> _assemblies;
        private readonly List<Type> _filters;
        private readonly List<string> _paths;

        /// <summary>
        /// Api应用供应商
        /// </summary>
        public HostAppliactionProvider()
        {
            _assemblies = new List<Assembly>();
            _paths = new List<string>();
            _filters = new List<Type>();
        }

        /// <summary>
        /// 导入路径配置
        /// </summary>
        protected virtual void OnConfigurePath(IList<string> paths) { }

        /// <summary>
        /// 导入路径配置
        /// </summary>
        protected virtual void OnConfigureAssembly(IList<Assembly> assemblies) { }

        /// <summary>
        /// 配置应用
        /// </summary>
        /// <param name="app"></param>
        public override void OnConfigureApplication(WebApplication app)
        {
            base.OnConfigureApplication(app);

            #region 添加Swagger支持
            if (base.HostConfig.IsSwagger)
            {
                // 使用Swagger
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    foreach (var swagger in base.HostConfig.Swaggers)
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
                if (base.HostConfig.IsRazorPageSupport) endpoints.MapRazorPages();
                // 映射所有控制器
                if (base.HostConfig.IsControllerSupport) endpoints.MapControllers();
            });
            #endregion
        }

        /// <summary>
        /// 配置依赖
        /// </summary>
        /// <param name="dependency"></param>
        protected override void OnConfigureDependency(IDependencyManager dependency)
        {
            // 添加基础组件注入
            dependency.Register<IHttpContextAccessor, HttpContextAccessor>(Lifetimes.Singleton);
            dependency.Register<Logs.Dependency.ILogger>(sy.Logger.GetCurrentLogger());
            dependency.Register(typeof(IOptionConfig<>), typeof(OptionConfig<>), Lifetimes.Singleton);
            dependency.Register(HostConfig);
            dependency.Register(Configuration);
            // 添加程序集
            dependency.Includes(_assemblies);
            // 先执行自动注册
            dependency.RegisterAll();
            // 注册所有的模块
            dependency.AddModulers(_assemblies);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="builder"></param>
        public override void OnInitialize(WebApplicationBuilder builder)
        {
            base.OnInitialize(builder);

            #region 应用路径配置
            sy.Logger.Debug($"Server Start ...", LogEvents.Server);
            // 预处理寻址路径
            _paths.Clear();
            _paths.Add(sy.Assembly.ExecutionDirectory);
            foreach (var path in HostConfig.Paths) _paths.Add(sy.IO.GetFullPath(path));
            #endregion

            #region 加载模块
            sy.Logger.Debug($"Import Modules ...", LogEvents.Server);
            OnConfigurePath(_paths);
            _assemblies.AddFromHostConfig(HostConfig, _paths);
            OnConfigureAssembly(_assemblies);
            #endregion

        }

        /// <summary>
        /// 服务配置
        /// </summary>
        /// <param name="services"></param>
        public override void OnConfigureServices(IServiceCollection services)
        {
            base.OnConfigureServices(services);

            // 输出服务注册日志
            sy.Logger.Debug($"Api services configure start ...", LogEvents.Services);

            #region 添加控制器配置
            // 添加服务配置
            ServiceOption option = new ServiceOption();
            option.RouteUrl = "/app";
            option.AddAssemblies(_assemblies);

            // 根据配置添加所有的控制器
            if (base.HostConfig.IsControllerSupport)
            {
                services.AddControllers(options =>
                {
                    // 添加过滤器
                    sy.Logger.Debug($"Add {typeof(ApiActionFilter).FullName}", LogEvents.Filter);
                    options.Filters.Add<ApiActionFilter>();
                    sy.Logger.Debug($"Add {typeof(ApiAsyncActionFilter).FullName}", LogEvents.Filter);
                    options.Filters.Add<ApiAsyncActionFilter>();
                    foreach (var filter in _filters)
                    {
                        sy.Logger.Debug($"Add {filter.FullName}", LogEvents.Filter);
                        options.Filters.Add(filter);
                    }
                    // 添加约定器
                    options.Conventions.Add(new ServiceApplicationModelConvention(option));
                    options.Conventions.Add(new ServiceActionModelConvention());
                }, _assemblies).ConfigureApplicationPartManager(pm =>
                {
                    // 添加自定义控制器
                    pm.FeatureProviders.Add(new ServiceControllerFeatureProvider(option));
                });
            }
            #endregion

            #region 添加页面配置
            // 根据配置添加所有的控制器
            if (base.HostConfig.IsRazorPageSupport)
            {
                services.AddRazorPages(_assemblies);
            }
            #endregion

            #region 添加Swagger配置
            if (base.HostConfig.IsSwagger)
            {
                services.AddSwaggerGen(options =>
                {
                    foreach (var swagger in base.HostConfig.Swaggers)
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
                        var swagger = base.HostConfig.Swaggers.Where(d => d.Name == docName).FirstOrDefault();
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
            if (base.HostConfig.IsRazorPageSupport) services.AddRazorPages();
            #endregion

            // 输出服务注册日志
            sy.Logger.Debug($"Api services configure completed.", LogEvents.Services);
        }
    }
}
