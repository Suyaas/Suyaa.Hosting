using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Suyaa.Hosting.Common.Configures;
using Suyaa.Hosting.Common.Configures.Helpers;
using Suyaa.Hosting.Multilingual.Configures;

namespace Suyaa.Hosting.Multilingual.Helpers
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
        public static WebApplicationBuilder AddMultilingualConfigure(this WebApplicationBuilder builder)
        {
            // 添加多语言配置
            builder.AddConfigure<MultilingualConfig>();
            return builder;
        }
    }
}
