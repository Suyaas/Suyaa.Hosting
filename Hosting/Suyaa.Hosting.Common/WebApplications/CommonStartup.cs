using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Suyaa.Hosting.Common.ActionFilters.Helpers;
using Suyaa.Hosting.Common.Configures;
using Suyaa.Hosting.Common.Configures.Helpers;
using Suyaa.Hosting.Common.DependencyInjection;
using Suyaa.Hosting.Common.DependencyInjection.Dependency;
using Suyaa.Hosting.Common.DependencyInjection.Helpers;
using Suyaa.Hosting.Common.Modules.Helpers;
using Suyaa.Hosting.Common.Resources;
using Suyaa.Hosting.Infrastructure.Assemblies;
using Suyaa.Hosting.Infrastructure.Assemblies.Dependency;
using Suyaa.Hosting.Infrastructure.Assemblies.Helpers;
using Suyaa.Hosting.Infrastructure.Exceptions;
using Suyaa.Hosting.Infrastructure.WebApplications;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Suyaa.Hosting.Common.WebApplications
{
    /// <summary>
    /// Web应用通用启动器
    /// </summary>
    public class CommonStartup : BaseStartup
    {
        // 变量定义
        private readonly List<Assembly> _assemblies;
        private HostConfig? _hostConfig;

        /// <summary>
        /// 主机配置
        /// </summary>
        public HostConfig HostConfig => _hostConfig ?? throw new HostException("Host config not found.");

        /// <summary>
        /// 所有程序集
        /// </summary>
        public IList<Assembly> Assemblies => _assemblies;

        /// <summary>
        /// Web应用基础供应商
        /// </summary>
        public CommonStartup()
        {
            _assemblies = new List<Assembly>();
        }

        #region 继承方法

        /// <summary>
        /// 创建依赖管理器
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        protected virtual IDependencyManager OnDependencyManagerCreating(IServiceCollection services)
        {
            return DependencyManager.Create(services);
        }

        /// <summary>
        /// 依赖配置
        /// </summary>
        /// <param name="dependencyManager"></param>
        protected virtual void OnConfigureDependency(IDependencyManager dependencyManager)
        {
            // 注册配置集合
            dependencyManager.RegisterInstance(Configuration);
            // 注册可选配置
            dependencyManager.AddOptionConfigure();
            // 注册所有的切片
            dependencyManager.AddActionFilters();
        }

        #endregion

        /// <summary>
        /// 服务配置
        /// </summary>
        /// <param name="app"></param>
        protected override void OnConfigureApplication(WebApplication app)
        {
            // 兼容开发模式
            if (app.Environment.IsDevelopment())
            {
                // 使用错误页
                app.UseDeveloperExceptionPage();
                // 使用控制台日志输出
                sy.Logger.Factory.UseConsole();
                // 调试日志输出
                sy.Logger.Factory.UseStringAction(message => { Debug.WriteLine(message); });
                // 创建一个新的日志记录器
                sy.Logger.Create();
            }
            else
            {
                // 兼容Https
                app.UseHsts();
                // 使用http跳转https
                app.UseHttpsRedirection();
            }
        }

        /// <summary>
        /// 服务配置
        /// </summary>
        /// <param name="services"></param>
        protected override void OnConfigureServices(IServiceCollection services)
        {
            // 创建依赖管理器
            IDependencyManager dependencyManager = OnDependencyManagerCreating(services);
            // 注册所有的标准依赖
            dependencyManager
                .RegisterImplementationInterfaces<IDependencySingleton>(Lifetimes.Singleton)
                .RegisterImplementationInterfaces<IDependencyExclusive>(Lifetimes.Exclusive)
                .RegisterImplementationInterfaces<IDependencyTransient>(Lifetimes.Transient);
            // 注册所有的模块
            dependencyManager.AddModulers(this.Assemblies);
            // 执行外部注册
            OnConfigureDependency(dependencyManager);
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
        /// 构建器配置
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnConfigureBuilder(WebApplicationBuilder builder)
        {
            base.OnConfigureBuilder(builder);

            // 添加所有配置信息
            builder.AddConfigures();

            #region 读取配置
            // 加载Suyaa配置
            sy.Logger.Debug("Load Hosting Config ...");
            _hostConfig = Configuration.GetHostConfig();
            if (_hostConfig is null) throw new NullException<HostConfig>();
            #endregion

            #region 重新配置日志
            // 注册文件日志
            sy.Logger.Factory.UseFile(sy.IO.GetFullPath(_hostConfig.LogPath));
            // 更新日志记录器
            sy.Logger.Create();
            #endregion

            #region 应用路径配置
            sy.Logger.Debug($"Server Start ...", LogEvents.Server);
            // 预处理寻址路径
            List<string> paths = new List<string>() { sy.Assembly.ExecutionDirectory };
            foreach (var path in HostConfig.Paths) paths.Add(sy.IO.GetFullPath(path));
            OnConfigurePath(paths);
            #endregion

            #region 加载模块
            sy.Logger.Debug($"Import Modules ...", LogEvents.Server);
            _assemblies.ImportFromHostConfig(HostConfig, paths);
            OnConfigureAssembly(_assemblies);
            #endregion

            #region 注册程序集聚合依赖
            AssembliesProvider assembliesProvider = new AssembliesProvider(_assemblies);
            AssembliesFactory assembliesFactory = new AssembliesFactory(assembliesProvider);
            builder.Services.AddSingleton<IAssembliesFactory>(assembliesFactory);
            #endregion

        }
    }
}
