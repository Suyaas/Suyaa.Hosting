using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Suyaa.Hosting.Common.Configures;
using Suyaa.Hosting.Common.Configures.Helpers;
using Suyaa.Hosting.Common.DependencyInjection;
using Suyaa.Hosting.Common.DependencyInjection.Dependency;
using Suyaa.Hosting.Infrastructure.Exceptions;
using Suyaa.Hosting.Infrastructure.WebApplications;
using System.Diagnostics;

namespace Suyaa.Hosting.Common.WebApplications
{
    /// <summary>
    /// Web应用通用启动器
    /// </summary>
    public class CommonStartup : BaseStartup
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
        public CommonStartup()
        {
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
        /// <param name="dependency"></param>
        protected virtual void OnConfigureDependency(IDependencyManager dependency) { }

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
            IDependencyManager dependency = OnDependencyManagerCreating(services);
            // 执行外部注册
            OnConfigureDependency(dependency);
        }

        /// <summary>
        /// 构建器配置
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnConfigureBuilder(WebApplicationBuilder builder)
        {
            _configuration = builder.Configuration;

            // 添加所有配置信息
            builder.AddConfigures();

            #region 配置日志
            // 加载Suyaa配置
            sy.Logger.Debug("Load Hosting Config ...");
            // 调试日志输出
            sy.Logger.Factory.UseStringAction(message => { Debug.WriteLine(message); });
            // 执行配置
            sy.Safety.Invoke(() =>
            {
                _hostConfig = _configuration.GetHostConfig();
                // 注册文件日志
                if (_hostConfig != null) sy.Logger.Factory.UseFile(sy.IO.GetFullPath(_hostConfig.LogPath));
            }, ex => { sy.Logger.Error(ex.ToString()); });
            if (_hostConfig is null) return;
            // 更新日志记录器
            sy.Logger.Create();
            #endregion

        }
    }
}
