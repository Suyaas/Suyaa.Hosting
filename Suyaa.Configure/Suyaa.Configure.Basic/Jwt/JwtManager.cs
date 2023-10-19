using Suyaa.Configure.Basic.Dependency;
using Suyaa.DependencyInjection;
using Suyaa.Hosting.Jwt;
using Suyaa.Hosting.Jwt.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Configure.Basic.Jwt
{
    /// <summary>
    /// Jwt管理器
    /// </summary>
    public class JwtManager : JwtManager<JwtInfo>, IJwtManager
    {
        /// <summary>
        /// Jwt管理器
        /// </summary>
        public JwtManager(IDependencyManager dependency) : base(dependency)
        {
        }
    }
}
