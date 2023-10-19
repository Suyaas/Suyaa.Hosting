using Suyaa.Hosting.Jwt.ActionFilters;
using Suyaa.Hosting.Jwt.Attributes;

namespace Suyaa.Configure.Basic.Jwt
{
    /// <summary>
    /// Jwt认证
    /// </summary>
    public class JwtAuthorizeAttribute : Suyaa.Hosting.Jwt.Attributes.JwtAuthorizeAttribute
    {
        /// <summary>
        /// Jwt认证
        /// </summary>
        public JwtAuthorizeAttribute() : base(typeof(JwtInfo)) { }
    }
}
