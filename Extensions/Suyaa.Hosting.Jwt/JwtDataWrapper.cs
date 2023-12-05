using Suyaa.Hosting.Jwt.Dependency;

namespace Suyaa.Hosting.Jwt
{
    /// <summary>
    /// Jwt数据包裹层
    /// </summary>
    public class JwtDataWrapper
    {
        /// <summary>
        /// Jwt数据包裹层
        /// </summary>
        /// <param name="jwtData"></param>
        public JwtDataWrapper(IJwtData jwtData)
        {
            JwtData = jwtData;
        }

        /// <summary>
        /// Jwt数据
        /// </summary>
        public IJwtData JwtData { get; }
    }
}
