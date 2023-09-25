using Suyaa.Hosting.Kernel.Dependency;

namespace Suyaa.Configure.Basic.Jwt
{
    /// <summary>
    /// Jwt用户信息
    /// </summary>
    public class JwtInfo : IJwtData
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// 用户账户
        /// </summary>
        public string UserAccount { get; set; } = string.Empty;
    }
}
