using Suyaa.Configure.Basic.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Configure.Cores.Users.Dto
{
    /// <summary>
    /// 用户登录出参
    /// </summary>
    public class UserLoginOutput 
    {
        /// <summary>
        /// Jwt令牌
        /// </summary>
        public string Token { get; set; } = string.Empty;
        /// <summary>
        /// 续订时间
        /// </summary>
        public long RenewalTime { get; set; } = 0;
    }
}
