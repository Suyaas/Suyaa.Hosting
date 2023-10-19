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
    /// Jwt数据供应商
    /// </summary>
    public sealed class JwtInfoProvider : IJwtDataProvider<JwtInfo>
    {
        #region DI注入

        private readonly IDependencyManager _dependencyManager;

        public JwtInfoProvider(
            IDependencyManager dependencyManager
            )
        {
            _dependencyManager = dependencyManager;
            this.Builder = _dependencyManager.Resolve<IJwtBuilder<JwtInfo>>();
        }

        #endregion

        /// <summary>
        /// 构建器
        /// </summary>
        /// <returns></returns>
        public IJwtBuilder<JwtInfo> Builder { get; }

        public JwtInfo CreateJwtData()
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
