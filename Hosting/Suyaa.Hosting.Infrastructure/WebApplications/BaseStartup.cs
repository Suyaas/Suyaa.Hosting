using Suyaa.Hosting.Infrastructure.WebApplications.Dependency;

namespace Suyaa.Hosting.Infrastructure.WebApplications
{
    /// <summary>
    /// Web应用基础启动器
    /// </summary>
    public class BaseStartup : IWebApplicationStartup
    {
        /// <summary>
        /// 应用配置
        /// </summary>
        /// <param name="app"></param>
        protected virtual void OnConfigureApplication(WebApplication app) { }

        /// <summary>
        /// 应用配置
        /// </summary>
        /// <param name="app"></param>
        public void ConfigureApplication(WebApplication app)
        {
            this.OnConfigureApplication(app);
        }

        /// <summary>
        /// 构建器配置
        /// </summary>
        /// <param name="builder"></param>
        protected virtual void OnConfigureBuilder(WebApplicationBuilder builder) { }

        /// <summary>
        /// 构建器配置
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureBuilder(WebApplicationBuilder builder)
        {
            this.OnConfigureBuilder(builder);
        }

        /// <summary>
        /// 服务配置
        /// </summary>
        /// <param name="services"></param>
        protected virtual void OnConfigureServices(IServiceCollection services) { }

        /// <summary>
        /// 服务配置
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            this.OnConfigureServices(services);
        }
    }
}
