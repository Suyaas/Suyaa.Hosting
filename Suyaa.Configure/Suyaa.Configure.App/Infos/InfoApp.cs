using Microsoft.AspNetCore.Mvc;
using Suyaa.Configure.Basic.Dependency;
using Suyaa.Configure.Basic.Jwt;
using Suyaa.Hosting.Jwt.Dependency;
using Suyaa.Hosting.Services;

namespace Suyaa.Configure.Apps.Infos
{
    /// <summary>
    /// 信息
    /// </summary>
    public sealed class InfoApp : ServiceApp
    {
        private readonly IJwtManager _jwtManager;

        /// <summary>
        /// 信息
        /// </summary>
        public InfoApp(
            IJwtManager jwtManager
            )
        {
            _jwtManager = jwtManager;
        }

        /// <summary>
        /// 获取版本号
        /// </summary>
        /// <returns></returns>
        public string GetVersion()
        {
            return sy.Assembly.Version;
        }

        /// <summary>
        /// 获取Jwt
        /// </summary>
        /// <returns></returns>
        public string GetJwt()
        {
            var jwtData = _jwtManager.Provider.CreateJwtData();
            return _jwtManager.Provider.Builder.CreateToken(jwtData).Token;
        }
    }
}
