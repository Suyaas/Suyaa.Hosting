using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Suyaa.Hosting.Jwt.Dependency;

namespace Suyaa.Hosting.Jwt
{
    /// <summary>
    /// Jwt数据供应商
    /// </summary>
    public class JwtDataProvider : IJwtDataProvider
    {
        /// <summary>
        /// 创建Jwt数据
        /// </summary>
        /// <returns></returns>
        public IJwtData CreateJwtData()
        {
            return new JwtData()
            {
                TenantId = 0,
                Uid = "",
            };
        }
    }
}
