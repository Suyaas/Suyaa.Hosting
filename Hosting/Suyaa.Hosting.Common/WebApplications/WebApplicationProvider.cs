using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Suyaa.Hosting.Common.DependencyManager.Dependency;
using Suyaa.Hosting.Configures;
using Suyaa.Hosting.Kernel;
using Suyaa.Hosting.Kernel.Dependency;
using Suyaa.Hosting.Kernel.Helpers;
using System.Diagnostics;

namespace Suyaa.Hosting.Common.WebApplications
{
    /// <summary>
    /// Web应用基础供应商
    /// </summary>
    public abstract class WebApplicationProvider : IWebApplicationProvider
    {
        // 变量定义
        private HostConfig? _hostConfig;
        private IConfiguration? _configuration;

        /// <summary>
        /// 主机配置
        /// </summary>
        public HostConfig HostConfig => _hostConfig ?? throw new HostException("Host config not found.");

        /// <summary>
        /// 主机配置
        /// </summary>
        public IConfiguration Configuration => _configuration ?? throw new HostException("Configuration not found.");

        /// <summary>
        /// Web应用基础供应商
        /// </summary>
        public WebApplicationProvider()
        {
        }

        #region 继承方法

        /// <summary>
        /// 创建依赖管理器
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        protected abstract IDependencyManager OnDependencyManagerCreating(IServiceCollection services);

        /// <summary>
        /// 依赖配置
        /// </summary>
        /// <param name="dependency"></param>
        protected abstract void OnConfigureDependency(IDependencyManager dependency);

        #endregion

        /// <summary>
        /// 服务配置
        /// </summary>
        /// <param name="app"></param>
        public virtual void OnConfigureApplication(WebApplication app)
        {
            // 兼容开发模式
            if (app.Environment.IsDevelopment())
            {
                // 使用错误页
                app.UseDeveloperExceptionPage();
                // 使用控制台日志输出
                sy.Logger.Factory.UseConsole();
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
        public virtual void OnConfigureServices(IServiceCollection services)
        {
            // 创建依赖管理器
            IDependencyManager dependency = OnDependencyManagerCreating(services);
            sy.Hosting.DependencyManager = dependency;
            // 执行外部注册
            OnConfigureDependency(dependency);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="builder"></param>
        public virtual void OnInitialize(WebApplicationBuilder builder)
        {
            _configuration = builder.Configuration;

            #region 配置日志
            // 加载Suyaa配置
            sy.Logger.Debug("Load Hosting Config ...");
            // 调试日志输出
            sy.Logger.Factory.UseStringAction(message => { Debug.WriteLine(message); });
            // 执行配置
            sy.Hosting.LogInvoke(() =>
            {
                _hostConfig = _configuration.GetHostConfig();
                // 注册文件日志
                if (_hostConfig != null) sy.Logger.Factory.UseFile(sy.IO.GetFullPath(_hostConfig.LogPath));
            });
            if (_hostConfig is null) return;
            // 更新日志记录器
            sy.Logger.Create();
            #endregion

        }
    }
}
