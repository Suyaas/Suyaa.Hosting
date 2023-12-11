using Suyaa.Configure.Basic.AppAuthorize.Dependency;

namespace Suyaa.Configure.Basic.AppAuthorize
{
    /// <summary>
    /// 应用信息
    /// </summary>
    public class AppInfo : IAppInfo, IDependencyTransient
    {
        /// <summary>
        /// 应用Id
        /// </summary>
        public string AppId { get; set; } = string.Empty;
        /// <summary>
        /// 应用密钥
        /// </summary>
        public string AppKey { get; set; } = string.Empty;
    }
}
