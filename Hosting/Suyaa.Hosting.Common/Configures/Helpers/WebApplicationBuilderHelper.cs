using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Suyaa.Configure;

namespace Suyaa.Hosting.Common.Configures.Helpers
{
    /// <summary>
    /// Web应用构建器助手
    /// </summary>
    public static class WebApplicationBuilderHelper
    {
        /// <summary>
        /// 添加单个配置的支持
        /// </summary>
        /// <typeparam name="TConfig"></typeparam>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static WebApplicationBuilder AddConfigure<TConfig>(this WebApplicationBuilder builder)
            where TConfig : class, IConfig, new()
        {
            // 添加配置
            var source = builder.Configuration.AddConfigurationSource<TConfig>();
            var config = source.GetConfig();
            // 注入依赖
            if (config != null) builder.Services.AddSingleton(config);
            return builder;
        }

        /// <summary>
        /// 添加配置相关的所有功能
        /// </summary>
        /// <returns></returns>
        public static WebApplicationBuilder AddConfigures(this WebApplicationBuilder builder)
        {
            // 添加主机配置
            builder.AddConfigure<HostConfig>();
            // 添加kestrelConfig配置
            builder.AddConfigure<KestrelConfig>();
            return builder;
        }
    }
}
