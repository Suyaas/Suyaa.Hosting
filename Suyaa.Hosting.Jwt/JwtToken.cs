using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Hosting.Jwt
{
    /// <summary>
    /// Jwt令牌
    /// </summary>
    public class JwtToken
    {
        /// <summary>
        /// 令牌
        /// </summary>
        public string Token { get; set; } = string.Empty;
        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime Expires { get; set; } = DateTime.Now;
    }
}
