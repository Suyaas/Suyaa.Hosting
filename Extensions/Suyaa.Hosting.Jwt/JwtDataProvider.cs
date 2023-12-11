using Suyaa.Hosting.Common.DependencyInjection.Dependency;
using Suyaa.Hosting.Common.DependencyInjection.Helpers;
using Suyaa.Hosting.Jwt.Dependency;

namespace Suyaa.Hosting.Jwt
{
    /// <summary>
    /// Jwt数据供应商
    /// </summary>
    public class JwtDataProvider : IJwtDataProvider<JwtData>
    {
        #region DI注入

        private readonly IDependencyManager _dependencyManager;
        private IJwtBuilder<JwtData>? _jwtBuilder;

        /// <summary>
        /// Jwt数据供应商
        /// </summary>
        /// <param name="dependencyManager"></param>
        public JwtDataProvider(
            IDependencyManager dependencyManager
            )
        {
            _dependencyManager = dependencyManager;
        }

        #endregion

        /// <summary>
        /// 创建构建器
        /// </summary>
        /// <returns></returns>
        public IJwtBuilder<JwtData> Builder => _jwtBuilder ??= _dependencyManager.ResolveRequired<IJwtBuilder<JwtData>>();

        /// <summary>
        /// 创建Jwt数据
        /// </summary>
        /// <returns></returns>
        public JwtData CreateJwtData()
        {
            return new JwtData()
            {
                TenantId = 0,
                Uid = "",
            };
        }
    }
}
