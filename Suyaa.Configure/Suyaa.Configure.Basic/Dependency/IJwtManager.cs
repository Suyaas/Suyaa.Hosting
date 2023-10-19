using Suyaa.Configure.Basic.Jwt;
using Suyaa.Hosting.Jwt.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Configure.Basic.Dependency
{
    /// <summary>
    /// Jwt管理器
    /// </summary>
    public interface IJwtManager : IJwtManager<JwtInfo>
    {

    }
}
