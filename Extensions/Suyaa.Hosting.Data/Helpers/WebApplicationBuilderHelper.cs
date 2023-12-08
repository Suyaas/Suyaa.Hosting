using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Suyaa.Configure;
using Suyaa.Hosting.Data.Configures;

namespace Suyaa.Hosting.Common.Configures.Helpers
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
        public static WebApplicationBuilder AddDatabaseConfigure(this WebApplicationBuilder builder)
        {
            // 添加数据库配置
            builder.AddConfigure<DatabaseConfig>();
            return builder;
        }
    }
}
