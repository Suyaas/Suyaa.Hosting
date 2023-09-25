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
        /// 用户Id
        /// </summary>
        public long UserId { get; set; } = 0;
    }
}