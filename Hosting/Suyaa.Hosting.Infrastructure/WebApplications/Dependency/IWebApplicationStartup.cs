namespace Suyaa.Hosting.Infrastructure.WebApplications.Dependency
{
    /// <summary>
    /// Web应用启动
    /// </summary>
    public interface IWebApplicationStartup
    {
        /// <summary>
        /// 构建器配置
        /// </summary>
        void ConfigureBuilder(WebApplicationBuilder builder);

        /// <summary>
        /// 服务配置
        /// </summary>
        /// <param name="services"></param>
        void ConfigureServices(IServiceCollection services);

        /// <summary>
        /// 应用配置
        /// </summary>
        /// <param name="app"></param>
        void ConfigureApplication(WebApplication app);
    }
}
