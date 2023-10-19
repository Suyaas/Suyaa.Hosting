using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Hosting.Jwt.Options
{
    /// <summary>
    /// Jwt配置项
    /// </summary>
    public class JwtOption
    {
        /// <summary>
        /// Jwt交互令牌密钥名称
        /// </summary>
        public string TokenName { get; set; } = "Jwt-Token";
        /// <summary>
        /// Jwt交互令牌密钥
        /// </summary>
        public string TokenKey { get; set; } = "0b26efe2c64e4eb8ae8e97384935cb2c";
        /// <summary>
        /// Jwt交互令牌路由
        /// </summary>
        public string RouteName { get; set; } = "Jwt";
        /// <summary>
        /// 是否Header支持
        /// </summary>
        public bool IsHeaderSupported { get; set; } = true;
        /// <summary>
        /// 是否Cookie支持
        /// </summary>
        public bool IsCookieSupported { get; set; } = true;
    }
}
