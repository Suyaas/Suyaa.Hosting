using Suyaa.Hosting.Infrastructure.WebApplications.Dependency;

namespace Suyaa.Hosting.Infrastructure.WebApplications.Helpers
{
    /// <summary>
    /// Web应用构建器助手
    /// </summary>
    public static class WebApplicationBuilderHelper
    {
        /// <summary>
        /// 构建Web应用
        /// </summary>
        /// <typeparam name="TStartup"></typeparam>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static WebApplication Build<TStartup>(this WebApplicationBuilder builder) where TStartup : class, Dependency.IWebApplicationStartup
        {
            var startup = sy.Assembly.Create<TStartup>();
            // 构建器配置
            startup.ConfigureBuilder(builder);
            // 配置服务
            startup.ConfigureServices(builder.Services);
            // 构建应用
            var app = builder.Build();
            // 应用配置
            startup.ConfigureApplication(app);
            return app;
        }
    }
}
