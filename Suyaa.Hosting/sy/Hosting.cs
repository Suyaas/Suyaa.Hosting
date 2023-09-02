using Microsoft.Extensions.Hosting;
using Suyaa.Hosting;

namespace sy
{
    /// <summary>
    /// 主机服务
    /// </summary>
    public static class Hosting
    {
        // 服务供应商
        private static IServiceProvider? _services = null;

        /// <summary>
        /// 
        /// </summary>
        public static IServiceProvider Services
        {
            get
            {
                if (_services is null) throw new HostException("Services not found! Please check if \"sy.Hosting.CreateHost\" was called");
                return _services;
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
            _services = host.Services;
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
            _services = host.Services;
            return host;
        }


        /// <summary>
        /// 创建WehHost
        /// </summary>
        /// <typeparam name="TStartup"></typeparam>
        /// <param name="configureBuilder"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHost CreateHost<TStartup>(Action<IHostBuilder> configureBuilder, string[]? args = null)
            where TStartup : class
        {
            var builder = Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(
                webBuilder =>
                {
                    webBuilder.UseStartup<TStartup>();
                });
            // 配置
            configureBuilder(builder);
            var host = builder.Build();
            // 设置服务服务供应商
            _services = host.Services;
            return host;
        }
    }
}
