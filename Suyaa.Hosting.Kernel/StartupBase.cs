using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using static System.Net.WebRequestMethods;
using Suyaa;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Xml.XPath;
using Suyaa.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Suyaa.Hosting.Configures;
using Suyaa.Logs.Loggers;
using ILogger = Suyaa.Logs.ILogger;
using Suyaa.Hosting.Kernel.Configures;
using Suyaa.Hosting.Kernel.Dependency;
using Suyaa.Hosting.Kernel.ActionFilters;
using Suyaa.Hosting.Kernel.ApplicationModelConventions;
using Suyaa.Hosting.Kernel.Helpers;
using Suyaa.Hosting.Kernel.FeatureProviders;
using Suyaa.Hosting.Options;

namespace Suyaa.Hosting.Kernel
{
    /// <summary>
    /// 启动器
    /// </summary>
    public abstract class StartupBase
    {
        // 私有变量
        private readonly HostConfig _hostConfig;
        // 服务集合
        private IServiceCollection? _services;
        //private readonly I18n _i18n;

        #region [=====私有方法=====]

        // 加载库文件
        private void ImportLibrary(string path)
        {
            // 所有所有路径
            for (int i = 0; i < this.Paths.Count; i++)
            {
                string filePath = sy.IO.GetOSPathFormat(this.Paths[i] + path);
                if (sy.IO.FileExists(filePath))
                {
                    Import(Assembly.LoadFrom(filePath));
                }
            }
        }

        // 获取完整地址
        private string GetFullPath(string path)
        {
            if (path.StartsWith("~/"))
            {
                return sy.IO.GetExecutionPath(path.Substring(2));
            }
            else
            {
                return path;
            }
        }

        #endregion

        #region [=====继承方法=====]

        /// <summary>
        /// 初始化事件
        /// </summary>
        protected abstract void OnInitialize();

        /// <summary>
        /// 创建依赖管理器事件
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        protected abstract IDependencyManager OnDependencyManagerCreating(IServiceCollection services);

        /// <summary>
        /// 依赖配置事件
        /// </summary>
        /// <param name="dependency"></param>
        protected abstract void OnConfigureDependency(IDependencyManager dependency);

        /// <summary>
        /// 服务配置事件
        /// </summary>
        /// <param name="services"></param>
        protected virtual void OnConfigureServices(IServiceCollection services) { }

        /// <summary>
        /// 配置HTTP请求管道事件
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        protected abstract void OnConfigure(IApplicationBuilder app, IWebHostEnvironment env);

        #endregion

        #region [=====公共方法=====]

        /// <summary>
        /// 主机服务配置
        /// </summary>
        public HostConfig HostConfig => _hostConfig;

        ///// <summary>
        ///// 多语言支持
        ///// </summary>
        //public I18n I18n => _i18n;

        ///// <summary>
        ///// 多语言配置信息
        ///// </summary>
        //public JsonConfigManager<I18nConfig> I18nConfigManager { get; }

        /// <summary>
        /// 寻址路径集合
        /// </summary>
        public List<string> Paths { get; set; }

        /// <summary>
        /// 获取程序集合
        /// </summary>
        public List<Assembly> Assembles { get; }

        /// <summary>
        /// 获取程序集合
        /// </summary>
        public List<Type> Filters { get; }

        /// <summary>
        /// 获取配置接口
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// 服务集合
        /// </summary>
        public IServiceCollection Services => _services ?? throw new HostException("No IServiceCollection found.");

        /// <summary>
        /// 导入程序集
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public StartupBase Import(Assembly? assembly)
        {
            if (assembly is null) return this;
            if (!this.Assembles.Contains(assembly)) this.Assembles.Add(assembly);
            return this;
        }

        /// <summary>
        /// 导入程序集
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public StartupBase Import(string path)
        {
            ImportLibrary(path);
            return this;
        }

        /// <summary>
        /// 导入程序集
        /// </summary>
        /// <param name="tp"></param>
        /// <returns></returns>
        public StartupBase Import(Type? tp)
        {
            return Import(tp?.Assembly);
        }

        /// <summary>
        /// 导入程序集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public StartupBase Import<T>() where T : class
        {
            return Import(typeof(T).Assembly);
        }

        /// <summary>
        /// 导入程序集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public StartupBase AddFilter<T>() where T : IFilterMetadata
        {
            this.Filters.Add(typeof(T));
            return this;
        }

        /// <summary>
        /// 启动器
        /// </summary>
        /// <param name="configuration">配置</param>
        public StartupBase(IConfiguration configuration)
        {
            // 属性设置
            this.Configuration = configuration;
            this.Assembles = new List<Assembly>();
            this.Filters = new List<Type>();
            // 加载Suyaa配置
            sy.Logger.Debug("Load Hosting Config ...");
            try
            {
                var hosting = configuration.GetSection("Hosting");
                if (hosting is null) throw new HostException(string.Format("Configuration section '{0}' not found.", "Hosting"));
                _hostConfig = hosting.Get<HostConfig>();
                // 注册日志
                sy.Logger.GetCurrentLogger()
                    .Use(new FileLogger(GetFullPath(_hostConfig.LogPath)))
                    .Use((string message) => { Debug.WriteLine(message); });
            }
            catch (Exception ex)
            {
                sy.Logger.Error(ex.ToString());
                throw;
            }
            //_suyaaConfig = configuration.GetValue<SuyaaConfig>("Suyaa");
            //if (_suyaaConfig is null) throw new HostException(i18n.Content("Configuration section '{0}' not found.", "Suyaa"));

            //string suyaaPath = suyaa.GetValue<string>("Path");
            //if (suyaa is null) throw new HostException($"未找到'Suyaa.Path'配置项");
            //this.SuyaaConfigManager = new JsonConfigManager<SuyaaConfig>(GetFullPath(suyaaPath));
            //_suyaaConfig = this.SuyaaConfigManager.Config;
            //_hostConfig = suyaa.To(() =>
            //{
            //    HostConfig config = new HostConfig();
            //    config.Default();
            //    return config;
            //});
            sy.Logger.Debug($"Server Start ...", LogEvents.Server);
            // 预处理寻址路径
            this.Paths = new List<string>()
            {
                sy.Assembly.ExecutionDirectory
            };
            foreach (var path in _hostConfig.Paths) this.Paths.Add(GetFullPath(path));
            // 触发初始化事件
            this.OnInitialize();
            // 加载所有的程序集
            for (int i = 0; i < _hostConfig.Libraries.Count; i++)
            {
                ImportLibrary(_hostConfig.Libraries[i]);
            }
        }

        /// <summary>
        ///  注册服务到容器中
        /// </summary>
        /// <param name="services"></param>
        public virtual void ConfigureServices(IServiceCollection services)
        {
            // 服务
            _services = services;

            // 输出服务注册日志
            sy.Logger.Debug($"Services Configure Start ...", "Services");

            #region 添加跨域支持
            if (_hostConfig.IsCorsAll)
            {
                services.AddCors(d =>
                {
                    d.AddPolicy(CrosTypes.ALL, p =>
                    {
                        p
                        .SetIsOriginAllowed((host) => true)
                        .AllowAnyHeader()
                        .AllowCredentials()
                        .AllowAnyMethod();
                    });
                });
            }
            #endregion

            // 添加基础组件注入
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<ILogger>(sy.Logger.GetCurrentLogger());
            services.AddSingleton(typeof(IOptionConfig<>), typeof(OptionConfig<>));
            services.AddSingleton(_hostConfig);
            services.AddSingleton(Configuration);

            // 创建依赖管理器
            IDependencyManager dependency = this.OnDependencyManagerCreating(services);
            sy.Hosting.DependencyManager = dependency;

            // 先执行自动注册
            dependency.RegisterAll();

            // 注册所有的模块
            dependency.AddModulers(this.Assembles);

            // 添加服务配置
            ServiceOption option = new ServiceOption();
            option.RouteUrl = "/app";
            option.AddAssemblies(this.Assembles);

            // 根据配置添加所有的控制器
            services.AddControllers(options =>
            {
                // 添加过滤器
                sy.Logger.Debug($"Add {typeof(ApiActionFilter).FullName}", LogEvents.Filter);
                options.Filters.Add<ApiActionFilter>();
                sy.Logger.Debug($"Add {typeof(ApiAsyncActionFilter).FullName}", LogEvents.Filter);
                options.Filters.Add<ApiAsyncActionFilter>();
                foreach (var filter in this.Filters)
                {
                    sy.Logger.Debug($"Add {filter.FullName}", LogEvents.Filter);
                    options.Filters.Add(filter);
                }
                // 添加约定器
                options.Conventions.Add(new ServiceApplicationModelConvention(option));
                options.Conventions.Add(new ServiceActionModelConvention());
            }, this.Assembles).ConfigureApplicationPartManager(pm =>
            {
                // 添加自定义控制器
                pm.FeatureProviders.Add(new ServiceControllerFeatureProvider(option));
            });

            //services.AddSingleton(configuration);
            //services.AddSingleton(mapper);

            // 执行外部注册
            this.OnConfigureServices(services);
            this.OnConfigureDependency(dependency);

            // 输出服务注册日志
            sy.Logger.Debug($"Services Configure Completed.", "Services");
        }

        /// <summary>
        /// 配置HTTP请求管道
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // 输出应用注册日志
            sy.Logger.Debug($"Apps Configure Start ...", "Apps");

            // 添加跨域支持
            if (_hostConfig.IsCorsAll) app.UseCors(CrosTypes.ALL);

            // 兼容开发模式
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                sy.Logger.GetCurrentLogger().Use<ConsoleLogger>();
            }
            else
            {
                //app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();

                // 使用http跳转https
                app.UseHttpsRedirection();
            }

            // 添加友好错误显示
            //app.UseFriendlyException();

            // 使用交互信息
            //app.UseSession();

            // 使用静态文件
            //app.UseStaticFiles();

            // 使用路由及用户授权
            app.UseRouting();
            //app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapRazorPages();
                // 映射所有控制器
                endpoints.MapControllers();
            });

            // 执行外部管道注册
            this.OnConfigure(app, env);

            // 输出应用注册日志
            sy.Logger.Debug($"Apps Configure Completed.", "Apps");
        }

        #endregion

    }
}
