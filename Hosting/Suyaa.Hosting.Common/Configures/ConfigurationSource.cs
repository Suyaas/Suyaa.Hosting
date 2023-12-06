using Microsoft.Extensions.Configuration;
using Suyaa.Configure;

namespace Suyaa.Hosting.Common.Configures
{
    /// <summary>
    /// 配置源
    /// </summary>
    public class ConfigurationSource<TConfig> : FileConfigurationSource
        where TConfig : class, IConfig, new()
    {
        private ConfigurationProvider<TConfig>? _provider;

        /// <summary>
        /// 获取配置实体
        /// </summary>
        /// <returns></returns>
        public TConfig? GetConfig()
        {
            return _provider?.Config;
        }

        // 构建配置
        public override IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            EnsureDefaults(builder);
            _provider = new ConfigurationProvider<TConfig>(builder);
            return _provider;
        }
    }
}
