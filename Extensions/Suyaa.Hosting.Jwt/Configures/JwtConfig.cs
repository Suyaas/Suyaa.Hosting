using Suyaa.Configure;
using Suyaa.Hosting.Common.Configures.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Hosting.Jwt.Configures
{
    /// <summary>
    /// Jwt配置项
    /// </summary>
    [Configuration("Jwt")]
    public class JwtConfig : IConfig
    {
        /// <summary>
        /// Jwt交互令牌密钥名称
        /// </summary>
        public string TokenName { get; set; } = string.Empty;
        /// <summary>
        /// Jwt交互令牌密钥
        /// </summary>
        public string TokenKey { get; set; } = string.Empty;
        /// <summary>
        /// 是否Header支持
        /// </summary>
        public bool IsHeaderSupported { get; set; } = false;
        /// <summary>
        /// 是否Cookie支持
        /// </summary>
        public bool IsCookieSupported { get; set; } = false;

        /// <summary>
        /// 默认配置
        /// </summary>
        public void Default()
        {
            TokenName = "Jwt-Token";
            TokenKey = "0b26efe2c64e4eb8ae8e97384935cb2c";
            IsHeaderSupported = true;
            IsCookieSupported = true;
        }
    }
}
