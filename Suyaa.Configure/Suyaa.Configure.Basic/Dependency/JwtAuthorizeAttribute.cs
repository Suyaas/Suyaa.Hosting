using Suyaa.Hosting.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Configure.Basic.Dependency
{
    /// <summary>
    /// Jwt认证
    /// </summary>
    public class JwtAuthorizeAttribute : JwtAuthorizeBaseAttribute
    {
        /// <summary>
        /// Jwt认证
        /// </summary>
        public JwtAuthorizeAttribute() : base(typeof(JwtUserInfo)) { }
    }
}
