using Suyaa.Configure.Basic.Infos;
using Suyaa.Configure.Cores.Users;
using Suyaa.Configure.Cores.Users.Dto;
using Suyaa.Configure.Cores.Users.Sto;
using Suyaa.Configure.Entity.Projects;
using Suyaa.Data;
using Suyaa.Hosting;
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
        private readonly IJwtDataManager _jwtDataManager;
        private readonly II18n _i18n;

        /// <summary>
        /// 用户
        /// </summary>
        public JwtUserApp(
            IUserCore userCore,
            IJwtDataManager jwtDataManager,
            II18n i18n
            )
        {
            _userCore = userCore;
            _jwtDataManager = jwtDataManager;
            _i18n = i18n;
        }

        /// <summary>
        /// 获取Jwt信息
        /// </summary>
        /// <returns></returns>
        [Get]
        public async Task<JwtInfo> GetJwtInfo()
        {
            if (_jwtDataManager.Data is null) throw new HostFriendlyException(_i18n.Content("Jwt info not found."));
            return await Task.FromResult((JwtInfo)_jwtDataManager.Data);
        }
    }
}