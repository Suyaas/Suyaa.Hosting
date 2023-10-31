namespace Suyaa.Hosting.Kernel.Dependency
{
    /// <summary>
    /// Web应用供应商
    /// </summary>
    public interface IWebApplicationProvider
    {
        /// <summary>
        /// 初始化
        /// </summary>
        void OnInitialize(WebApplicationBuilder builder);

        /// <summary>
        /// 服务配置
        /// </summary>
        /// <param name="services"></param>
        void OnConfigureServices(IServiceCollection services);

        /// <summary>
        /// 应用配置
        /// </summary>
        /// <param name="app"></param>
        void OnConfigureApplication(WebApplication app);
    }
}
