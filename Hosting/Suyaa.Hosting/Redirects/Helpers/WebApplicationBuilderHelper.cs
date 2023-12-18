using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Suyaa.Configure;
using Suyaa.Hosting.Redirects.Configures;

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
        public static WebApplicationBuilder AddRedirectConfigure(this WebApplicationBuilder builder)
        {
            // 添加重定向配置
            builder.AddConfigure<RedirectConfig>();
            return builder;
        }
    }
}
