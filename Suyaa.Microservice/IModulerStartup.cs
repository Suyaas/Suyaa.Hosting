namespace Suyaa.Microservice
{
    /// <summary>
    /// 模块启动器
    /// </summary>
    public interface IModulerStartup
    {
        /// <summary>
        /// 服务注册
        /// </summary>
        /// <param name="services"></param>
        void ConfigureServices(IServiceCollection services);
    }
}
