using Suyaa.Hosting.Jwt.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Configure.Basic.Jwt
{
    public sealed class JwtDataProvider : IJwtDataProvider
    {
        public IJwtData CreateJwtData()
        {
            return new JwtInfo()
            {
                TenantId = 0,
                Uid = "",
                UserAccount = "",
            };
        }
    }
}
