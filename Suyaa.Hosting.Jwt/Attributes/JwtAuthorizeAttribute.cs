using Suyaa.Hosting.Jwt.ActionFilters;

namespace Suyaa.Hosting.Jwt.Attributes
{
    /// <summary>
    /// Jwt认证
    /// </summary>
    public class JwtAuthorizeAttribute : CustomAttribute
    {
        /// <summary>
        /// Jwt认证
        /// </summary>
        public JwtAuthorizeAttribute(Type type) : base(typeof(JwtAuthorizeFilter<>).MakeGenericType(type)) { }
    }
}
