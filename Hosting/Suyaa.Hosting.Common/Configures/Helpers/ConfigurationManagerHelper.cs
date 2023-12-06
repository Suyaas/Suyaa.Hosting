using Microsoft.Extensions.Configuration;
using Suyaa.Configure;

namespace Suyaa.Hosting.Common.Configures.Helpers
{
    /// <summary>
    /// 配置管理器助手
    /// </summary>
    public static class ConfigurationManagerHelper
    {
        /// <summary>
        /// 添加Kestrel配置
        /// </summary>
        /// <returns></returns>
        public static ConfigurationSource<TConfig> AddConfigurationSource<TConfig>(this ConfigurationManager manager)
            where TConfig : class, IConfig, new()
        {
            var source = new ConfigurationSource<TConfig>();
            manager.Sources.Add(source);
            return source;
        }
    }
}
