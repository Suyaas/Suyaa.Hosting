using Suyaa.Hosting.Kernel.Dependency;

namespace Suyaa.Configure.Basic.Jwt
{
    /// <summary>
    /// Jwt用户信息
    /// </summary>
    public class JwtInfo : IJwtData
    {
        /// <summary>
        /// 用户账户
        /// </summary>
        public string UserAccount { get; set; } = string.Empty;
        /// <summary>
        /// 用户标识
        /// </summary>
        public string Uid { get; set; } = string.Empty;
        /// <summary>
        /// 租户Id
        /// </summary>
        public int TenantId { get; set; } = 0;
    }
}
