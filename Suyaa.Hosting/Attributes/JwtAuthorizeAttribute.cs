using Suyaa.Hosting.ActionFilters;
using Suyaa.Hosting.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Hosting.Attributes
{
    /// <summary>
    /// Jwt认证
    /// </summary>
    public class JwtAuthorizeAttribute : CustomAttribute
    {
        /// <summary>
        /// Jwt认证
        /// </summary>
        public JwtAuthorizeAttribute() : base(typeof(JwtAuthorizeFilter)) { }
    }
}
