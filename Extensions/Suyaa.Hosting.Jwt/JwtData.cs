using Suyaa.Hosting.Jwt.Dependency;

namespace Suyaa.Hosting.Jwt
{

    /// <summary>
    /// Jwt信息
    /// </summary>
    public class JwtData : IJwtData
    {
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