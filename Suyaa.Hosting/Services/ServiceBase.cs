namespace Suyaa.Hosting.Services
{
    /// <summary>
    /// 服务基础
    /// </summary>
    public abstract class ServiceBase
    {
        /// <summary>
        /// 获取服务供应商
        /// </summary>
        public IServiceProvider Services => sy.Hosting.Services;
    }
}
