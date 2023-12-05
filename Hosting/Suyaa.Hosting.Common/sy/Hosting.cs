using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Suyaa.Hosting.Common.DependencyManager.Dependency;
using Suyaa.Hosting.Kernel;
using Suyaa.Hosting.Kernel.Dependency;

namespace sy
{
    /// <summary>
    /// 主机服务
    /// </summary>
    public static class Hosting
    {
        // 依赖管理器
        private static IDependencyManager? _dependency = null;

        /// <summary>
        /// 依赖管理器
        /// </summary>
        public static IDependencyManager DependencyManager
        {
            get
            {
                if (_dependency is null) throw new HostException("Services not found! Please check if \"sy.Hosting.CreateHost\" was called");
                return _dependency;
            }
            internal set
            {
                _dependency = value;
            }
        }

        /// <summary>
        /// 创建WehHost
        /// </summary>
        /// <typeparam name="TStartup"></typeparam>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHost CreateHost<TStartup>(string[]? args = null)
            where TStartup : class
        {
            var host = Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(
                webBuilder =>
                {
                    webBuilder.UseStartup<TStartup>();
                }).Build();
            // 设置服务服务供应商
            //_services = host.Services;
            return host;
        }


        /// <summary>
        /// 创建WehHost
        /// </summary>
        /// <typeparam name="TStartup"></typeparam>
        /// <param name="configure"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHost CreateHost<TStartup>(Action<IWebHostBuilder> configure, string[]? args = null)
            where TStartup : class
        {
            var host = Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(
                webBuilder =>
                {
                    // 执行配置
                    configure(webBuilder);
                    webBuilder.UseStartup<TStartup>();
                }).Build();
            // 设置服务服务供应商
            //_services = host.Services;
            return host;
        }


        /// <summary>
        /// 创建WehHost
        /// </summary>
        /// <typeparam name="TStartup"></typeparam>
        /// <param name="configure"></param>
        /// <param name="configureBuilder"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHost CreateHost<TStartup>(Action<IWebHostBuilder> configure, Action<IHostBuilder> configureBuilder, string[]? args = null)
            where TStartup : class
        {
            var builder = Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(
                webBuilder =>
                {
                    // 执行配置
                    configure(webBuilder);
                    webBuilder.UseStartup<TStartup>();
                });
            // 配置
            configureBuilder(builder);
            var host = builder.Build();
            // 设置服务服务供应商
            //_services = host.Services;
            return host;
        }

        /// <summary>
        /// 创建一个Web应用
        /// </summary>
        /// <typeparam name="TProvider"></typeparam>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static WebApplication CreateWebApplication<TProvider>(WebApplicationBuilder builder)
            where TProvider : class, IWebApplicationProvider, new()
        {
            // 创建供应商
            var provider = new TProvider();
            // 执行初始化
            provider.OnInitialize(builder);
            // 执行服务配置
            provider.OnConfigureServices(builder.Services);
            // 构建应用
            var app = builder.Build();
            // 配置应用
            provider.OnConfigureApplication(app);
            // 应用配置
            return app;
        }

        /// <summary>
        /// 创建一个Web应用
        /// </summary>
        /// <typeparam name="TProvider"></typeparam>
        /// <returns></returns>
        public static WebApplication CreateWebApplication<TProvider>(string[] args)
            where TProvider : class, IWebApplicationProvider, new()
        {
            // 新建一个构建器
            var builder = WebApplication.CreateBuilder(args);
            return CreateWebApplication<TProvider>(builder);
        }

        /// <summary>
        /// 创建一个Web应用
        /// </summary>
        /// <typeparam name="TProvider"></typeparam>
        /// <returns></returns>
        public static WebApplication CreateWebApplication<TProvider>(WebApplicationOptions options)
            where TProvider : class, IWebApplicationProvider, new()
        {
            // 新建一个构建器
            var builder = WebApplication.CreateBuilder(options);
            return CreateWebApplication<TProvider>(builder);
        }

        /// <summary>
        /// 执行并记录异常
        /// </summary>
        /// <param name="action"></param>
        public static void LogInvoke(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                sy.Logger.Error(ex.ToString());
                throw;
            }
        }

        /// <summary>
        /// 尝试执行并记录异常
        /// </summary>
        /// <param name="action"></param>
        public static void TryLogInvoke(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                sy.Logger.Error(ex.ToString());
            }
        }

        /// <summary>
        /// 执行并记录异常
        /// </summary>
        /// <param name="func"></param>
        public static async void LogInvokeAsync(Func<Task> func)
        {
            try
            {
                await func();
            }
            catch (Exception ex)
            {
                sy.Logger.Error(ex.ToString());
                throw;
            }
        }

        /// <summary>
        /// 尝试执行并记录异常
        /// </summary>
        /// <param name="func"></param>
        public static async void TryLogInvokeAsync(Func<Task> func)
        {
            try
            {
                await func();
            }
            catch (Exception ex)
            {
                sy.Logger.Error(ex.ToString());
            }
        }
    }
}
