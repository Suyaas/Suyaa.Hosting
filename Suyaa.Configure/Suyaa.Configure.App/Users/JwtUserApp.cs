using Suyaa.Configure.Basic.Infos;
using Suyaa.Configure.Cores.Users;
using Suyaa.Configure.Cores.Users.Dto;
using Suyaa.Configure.Cores.Users.Sto;
using Suyaa.Configure.Entity.Projects;
using Suyaa.Data;
using Suyaa.Hosting.Attributes;
using Suyaa.Hosting.Dependency;
using Suyaa.Hosting.Services;

namespace Suyaa.Configure.App.Users
{
    /// <summary>
    /// 用户
    /// </summary>
    [App("User")]
    [JwtAuthorize]
    public class JwtUserApp : ServiceApp
    {
        private readonly IUserCore _userCore;
        private readonly IJwtData _jwtData;

        /// <summary>
        /// 用户
        /// </summary>
        public JwtUserApp(
            IUserCore userCore,
            IJwtData jwtData
            )
        {
            _userCore = userCore;
            _jwtData = jwtData;
        }

        /// <summary>
        /// 获取Jwt信息
        /// </summary>
        /// <returns></returns>
        [Get]
        public async Task<JwtInfo> GetJwtInfo()
        {
            return await Task.FromResult((JwtInfo)_jwtData);
        }
    }
}