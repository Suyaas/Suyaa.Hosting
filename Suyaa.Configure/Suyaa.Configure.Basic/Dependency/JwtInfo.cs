using Suyaa.Hosting.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Configure.Basic.Dependency
{
    /// <summary>
    /// Jwt用户信息
    /// </summary>
    public class JwtUserInfo : IJwtData
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public long UserId { get; set; }
    }
}
