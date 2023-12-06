using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Suyaa;
using Suyaa.Hosting.Common.DependencyInjection.Dependency;
using Suyaa.Hosting.Infrastructure.Exceptions;
using System.Diagnostics;

namespace sy
{
    /// <summary>
    /// 主机服务
    /// </summary>
    public static class Hosting
    {
        // 依赖管理器
        //private static IDependencyManager? _dependency = null;

        /// <summary>
        /// 依赖管理器
        /// </summary>
        public static IDependencyManager DependencyManager
        {
            get
            {
                var dependencyManager = Suyaa.Hosting.Common.DependencyInjection.DependencyManager.GetCurrent();
                if (dependencyManager is null) throw new NotExistException<IDependencyManager>();
                //return _dependency;
                return dependencyManager;
            }
            //internal set
            //{
            //    _dependency = value;
            //}
        }

        /// <summary>
        /// 获取AspNetCore环境变量
        /// </summary>
        /// <returns></returns>
        public static string GetAspNetCoreEnvironment()
        {
            string key = "ASPNETCORE_ENVIRONMENT";
            string env = System.Environment.GetEnvironmentVariable(key) ?? "Prod";
            if (env == "Development") env = "Dev";
            if (System.Environment.GetEnvironmentVariable(key).IsNullOrWhiteSpace()) System.Environment.SetEnvironmentVariable(key, env);
            return env;
        }

        /// <summary>
        /// 获取模块基目录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string GetModulePath<T>()
            where T : class
        {
            using var processModule = Process.GetCurrentProcess().MainModule;
            var filePath = typeof(T).Assembly.Location;
            var folder = sy.IO.GetFolderPath(filePath);
            return folder;
        }

        /// <summary>
        /// 创建一个Web应用
        /// </summary>
        /// <typeparam name="TProvider"></typeparam>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static WebApplication CreateWebApplication<TProvider>(WebApplicationBuilder builder)
            where TProvider : class, Suyaa.Hosting.Infrastructure.WebApplications.Dependency.IWebApplicationStartup, new()
        {
            // 创建供应商
            var provider = new TProvider();
            // 执行初始化
            provider.ConfigureBuilder(builder);
            // 执行服务配置
            provider.ConfigureServices(builder.Services);
            // 构建应用
            var app = builder.Build();
            // 配置应用
            provider.ConfigureApplication(app);
            // 应用配置
            return app;
        }

        /// <summary>
        /// 创建一个Web应用
        /// </summary>
        /// <typeparam name="TProvider"></typeparam>
        /// <returns></returns>
        public static WebApplication CreateWebApplication<TProvider>(string[] args)
            where TProvider : class, Suyaa.Hosting.Infrastructure.WebApplications.Dependency.IWebApplicationStartup, new()
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
            where TProvider : class, Suyaa.Hosting.Infrastructure.WebApplications.Dependency.IWebApplicationStartup, new()
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
