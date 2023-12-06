using Microsoft.Extensions.Configuration;
using Suyaa.Configure;
using Suyaa.Hosting.Infrastructure.Exceptions;

namespace Suyaa.Hosting.Common.Configures.Helpers
{
    /// <summary>
    /// 配置助手
    /// </summary>
    public static class ConfigurationHelper
    {
        /// <summary>
        /// 获取配置
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="name"></param>
        public static T? GetConfig<T>(this IConfiguration configuration, string name)
            where T : class, IConfig
        {
            var hosting = configuration.GetSection(name);
            if (hosting is null) throw new HostException(string.Format("Configuration section '{0}' not found.", name));
            return hosting.Get<T>();
        }

        /// <summary>
        /// 获取配置
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static TConfig? GetConfigOrDefault<TConfig>(this IConfiguration configuration, string name)
            where TConfig : class, IConfig, new()
        {
            var hosting = configuration.GetSection(name);
            if (hosting is null)
            {
                var config = new TConfig();
                config.Default();
                return config;
            }
            return hosting.Get<TConfig>();
        }

        /// <summary>
        /// 获取主机配置
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static HostConfig? GetHostConfig(this IConfiguration configuration)
        {
            return configuration.GetConfig<HostConfig>("Hosting");
        }
    }
}
