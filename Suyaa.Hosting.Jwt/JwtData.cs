using Suyaa.DependencyInjection;
using Suyaa.Hosting.Kernel.Dependency;

namespace Suyaa.Hosting.Jwt
{

    /// <summary>
    /// Jwt信息
    /// </summary>
    public class JwtData : IJwtData, IDependencyTransient
    {
        /// <summary>
        /// 用户标识
        /// </summary>
        public string? Uid { get; set; }
        /// <summary>
        /// 租户Id
        /// </summary>
        public int? TenantId { get; set; }
    }
}