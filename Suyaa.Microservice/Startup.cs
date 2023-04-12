using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Suyaa.Configure;
using Suyaa.Exceptions;
using Suyaa.Logs.Loggers;
using Suyaa.Microservice.ActionFilters;
using Suyaa.Microservice.Configures;
using Suyaa.Microservice.Dependency;
using Suyaa.Microservice.Exceptions;
using Suyaa.Microservice.Extensions;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using static System.Net.WebRequestMethods;
using ILogger = Suyaa.Logs.ILogger;

namespace Suyaa.Microservice
{
    /// <summary>
    /// 启动器
    /// </summary>
    public class Startup
    {
        // 私有变量
        private readonly SuyaaConfig _suyaaConfig;

        //// 读取配置
        //private SuyaaConfig ReadSuyaaSetting(string path)
        //{
        //    try
        //    {
        //        // 不存在时创建配置文件
        //        if (!sy.IO.FileExists(path))
        //        {
        //            // 自动创建目录
        //            string? folder = System.IO.Path.GetDirectoryName(path);
        //            if (folder is null) throw new NullException($"路径'{path}'不合法");
        //            sy.IO.CreateFolder(folder);
        //            // 生成默认配置文件
        //            SuyaaConfig setting = new SuyaaConfig();
        //            setting.SaveToFile(path);
        //        }
        //        // 读取文件
        //        return sy.Configure.LoadJsonSettingFromFile<SuyaaConfig>(path) ?? new SuyaaConfig();
        //    }
        //    catch (Exception ex)
        //    {
        //        sy.Logger.Error(ex.ToString());
        //        return new SuyaaConfig();
        //    }
        //}

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

        #region [=====继承方法=====]

        /// <summary>
        /// 初始化事件
        /// </summary>
        protected virtual void OnInitialize() { }

        /// <summary>
        /// 服务注册事件
        /// </summary>
        /// <param name="services"></param>
        protected virtual void OnConfigureServices(IServiceCollection services) { }

        /// <summary>
        /// 配置HTTP请求管道事件
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        protected virtual void OnConfigure(IApplicationBuilder app, IWebHostEnvironment env) { }

        #endregion

        #region [=====公共方法=====]

        /// <summary>
        /// 舒雅服务配置
        /// </summary>
        public JsonConfigManager<SuyaaConfig> SuyaaConfigManager { get; }

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
        /// 导入程序集
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public Startup Import(Assembly? assembly)
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
        public Startup Import(string path)
        {
            ImportLibrary(path);
            return this;
        }

        /// <summary>
        /// 导入程序集
        /// </summary>
        /// <param name="tp"></param>
        /// <returns></returns>
        public Startup Import(Type? tp)
        {
            return Import(tp?.Assembly);
        }

        /// <summary>
        /// 导入程序集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Startup Import<T>() where T : class
        {
            return Import(typeof(T).Assembly);
        }

        /// <summary>
        /// 导入程序集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Startup AddFilter<T>() where T : IFilterMetadata
        {
            this.Filters.Add(typeof(T));
            return this;
        }

        /// <summary>
        /// 启动器
        /// </summary>
        /// <param name="configuration">配置</param>
        public Startup(IConfiguration configuration)
        {
            // 属性设置
            this.Configuration = configuration;
            this.Assembles = new List<Assembly>();
            this.Filters = new List<Type>();
            // 加载配置
            var suyaa = configuration.GetSection("Suyaa");
            if (suyaa is null) throw new MicroserviceException($"未找到'Suyaa'配置节点");
            string suyaaPath = suyaa.GetValue<string>("Path");
            if (suyaa is null) throw new MicroserviceException($"未找到'Suyaa.Path'配置项");
            this.SuyaaConfigManager = new JsonConfigManager<SuyaaConfig>(GetFullPath(suyaaPath));
            _suyaaConfig = this.SuyaaConfigManager.Config;
            // 注册日志
            sy.Logger.GetCurrentLogger()
                .Use(new FileLogger(GetFullPath(_suyaaConfig.LogPath)))
                .Use((string message) => { Debug.WriteLine(message); });
            sy.Logger.Debug($"Server Start ...", "Server");
            // 预处理寻址路径
            this.Paths = new List<string>()
            {
                sy.Assembly.ExecutionDirectory
            };
            foreach (var path in _suyaaConfig.Paths) this.Paths.Add(GetFullPath(path));
            // 触发初始化事件
            this.OnInitialize();
            // 加载所有的程序集
            for (int i = 0; i < _suyaaConfig.Libraries.Count; i++)
            {
                ImportLibrary(_suyaaConfig.Libraries[i]);
            }
        }

        /// <summary>
        ///  注册服务到容器中
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            // 输出服务注册日志
            sy.Logger.Debug($"Services Configure Start ...", "Services");

            #region 添加跨域支持
            if (_suyaaConfig.IsCorsAll)
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

            // 添加数据仓库依赖注入
            //services.AddDbRepository((optionsBuilder) => optionsBuilder.UseNpgsql("Host=localhost;Database=salesgirl;Username=postgres;Password=12345678"));

            // 添加注入
            services.AddSingleton<ILogger>(sy.Logger.GetCurrentLogger());
            services.AddSingleton<IConfig>(_suyaaConfig);

            // 根据配置添加所有的控制器
            services.AddControllers(options =>
            {
                sy.Logger.Debug($"Add {typeof(ApiActionFilter).FullName}", "Filters");
                options.Filters.Add<ApiActionFilter>();
                sy.Logger.Debug($"Add {typeof(ApiAsyncActionFilter).FullName}", "Filters");
                options.Filters.Add<ApiAsyncActionFilter>();
                foreach (var filter in this.Filters)
                {
                    sy.Logger.Debug($"Add {filter.FullName}", "Filters");
                    options.Filters.Add(filter);
                }
            }, this.Assembles);

            // 注册所有的模块
            services.AddModulers(this.Assembles);

            // 注入 Session
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.Cookie.Name = "Suyaa.Session";
                options.IdleTimeout = TimeSpan.FromSeconds(2000); // 设置session的过期时间
                options.Cookie.HttpOnly = true; // 设置在浏览器不能通过js获得该cookie的值 
            });

            //// 注入Swagger
            //services.AddSwaggerGen(options =>
            //{
            //    options.SwaggerDoc("all", new OpenApiInfo { Title = "All APIs", Version = "all" });
            //    var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            //    var directory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            //    var files = directory.GetFiles("*.xml");
            //    foreach (var file in files)
            //    {
            //        //if (file.Name.StartsWith("Suyaa."))
            //        options.IncludeXmlComments(file.FullName, true);
            //    }
            //    options.DocInclusionPredicate((docName, description) => true);
            //});

            #region 添加Swagger配置
            if (_suyaaConfig.IsSwagger)
            {
                foreach (var swagger in _suyaaConfig.Swaggers)
                {
                    services.AddSwaggerGen(options =>
                    {
                        options.SwaggerDoc(swagger.Name, new OpenApiInfo { Title = swagger.Description, Version = swagger.Name });
                        var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

                        var directory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
                        var files = directory.GetFiles("*.xml");
                        foreach (var file in files)
                        {
                            if (swagger.Keyword == "*" || file.Name.Contains(swagger.Keyword))
                                options.IncludeXmlComments(file.FullName, true);
                        }
                        options.DocInclusionPredicate((docName, description) => true);
                    });
                }
            }
            #endregion

            // 执行外部注册
            this.OnConfigureServices(services);

            // 输出服务注册日志
            sy.Logger.Debug($"Services Configure Completed.", "Services");
        }

        /// <summary>
        /// 配置HTTP请求管道
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // 输出应用注册日志
            sy.Logger.Debug($"Apps Configure Start ...", "Apps");

            // 添加跨域支持
            if (_suyaaConfig.IsCorsAll) app.UseCors(CrosTypes.ALL);

            #region 添加Swagger支持
            if (_suyaaConfig.IsSwagger)
            {
                // 使用Swagger
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    foreach (var swagger in _suyaaConfig.Swaggers)
                    {
                        options.SwaggerEndpoint($"/swagger/{swagger.Name}/swagger.json", swagger.Description);
                    }
                    //options.SwaggerEndpoint("/swagger/v1/swagger.json", "Suyaa Microservice API V1");
                    options.EnableFilter();
                });
            }
            #endregion

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
