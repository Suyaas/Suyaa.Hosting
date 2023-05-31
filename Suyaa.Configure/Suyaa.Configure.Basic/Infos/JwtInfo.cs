using Suyaa.Configure.Basic.Dependency;
using Suyaa.Hosting.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Configure.Basic.Infos
{
    /// <summary>
    /// Jwt用户信息
    /// </summary>
    public class JwtInfo : IJwtData
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public long UserId { get; set; }
    }
}
