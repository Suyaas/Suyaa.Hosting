using Microsoft.AspNetCore.Builder;
using Suyaa.Hosting.Common.Configures.Helpers;
using Suyaa.Hosting.Jwt.Configures;

namespace Suyaa.Hosting.Jwt.Helpers
{
    /// <summary>
    /// Web应用构建器助手
    /// </summary>
    public static class WebApplicationBuilderHelper
    {

        /// <summary>
        /// 添加数据库配置
        /// </summary>
        /// <returns></returns>
        public static WebApplicationBuilder AddJwtConfigure(this WebApplicationBuilder builder)
        {
            // 添加数据库配置
            builder.AddConfigure<JwtConfig>();
            return builder;
        }
    }
}
