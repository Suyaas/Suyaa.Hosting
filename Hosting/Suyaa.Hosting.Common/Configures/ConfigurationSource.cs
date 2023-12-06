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
        // 构建配置
        public override IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            EnsureDefaults(builder);
            return new ConfigurationProvider<TConfig>(builder);
        }
    }
}
