using Microsoft.AspNetCore.Builder;

namespace Suyaa.Hosting.Common.Configures.Helpers
{
    /// <summary>
    /// Web应用构建器助手
    /// </summary>
    public static class WebApplicationBuilderHelper
    {
        /// <summary>
        /// 添加配置相关的所有功能
        /// </summary>
        /// <returns></returns>
        public static WebApplicationBuilder AddConfigures(this WebApplicationBuilder builder)
        {
            //var source=builder.Configuration.Sources.Add(new )
            builder.Configuration
                .AddConfigurationSource<HostConfig>()
                .AddConfigurationSource<KestrelConfig>();
            return builder;
        }
    }
}
