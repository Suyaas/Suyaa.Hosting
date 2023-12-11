using Microsoft.Extensions.Configuration;
using Suyaa.Configure;

namespace Suyaa.Hosting.Common.Configures.Helpers
{
    /// <summary>
    /// 配置管理器助手
    /// </summary>
    public static class ConfigurationBuilderHelper
    {
        /// <summary>
        /// 添加Kestrel配置
        /// </summary>
        /// <returns></returns>
        public static ConfigurationSource<TConfig> AddConfigurationSource<TConfig>(this IConfigurationBuilder builder)
            where TConfig : class, IConfig, new()
        {
            var source = new ConfigurationSource<TConfig>();
            builder.Sources.Add(source);
            return source;
        }
    }
}
