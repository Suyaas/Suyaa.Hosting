using Microsoft.AspNetCore.Mvc;
using Suyaa.Hosting.Jwt.Dependency;
using Suyaa.Hosting.Services;

namespace Suyaa.Configure.Apps.Infos
{
    /// <summary>
    /// 信息
    /// </summary>
    public sealed class InfoApp : ServiceApp
    {
        private readonly IJwtDataProvider _jwtDataProvider;

        /// <summary>
        /// 信息
        /// </summary>
        public InfoApp(
            IJwtDataProvider jwtDataProvider
            )
        {
            _jwtDataProvider = jwtDataProvider;
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
            var jwtData = _jwtDataProvider.CreateJwtData();
            return sy.Jwt.CreateToken(jwtData).Token;
        }
    }
}
