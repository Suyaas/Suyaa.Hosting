using Suyaa.Configure.Basic.Dependency;
using Suyaa.Configure.Basic.Jwt;
using Suyaa.Configure.Cores.Users;
using Suyaa.Hosting.Dependency;
using Suyaa.Hosting.Jwt.Dependency;
using Suyaa.Hosting.Kernel;
using Suyaa.Hosting.Kernel.Attributes;
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
        private readonly IJwtManager _jwtManager;
        private readonly IMultilingualManager _i18n;

        /// <summary>
        /// 用户
        /// </summary>
        public JwtUserApp(
            IUserCore userCore,
            IJwtManager jwtManager,
            IMultilingualManager i18n
            )
        {
            _userCore = userCore;
            _jwtManager = jwtManager;
            _i18n = i18n;
        }

        /// <summary>
        /// 获取Jwt信息
        /// </summary>
        /// <returns></returns>
        [Get]
        public async Task<JwtInfo> GetJwtInfo()
        {
            var jwtData = _jwtManager.GetCurrentData();
            if (jwtData is null) throw new HostFriendlyException(_i18n.Content("Jwt info not found."));
            return await Task.FromResult((JwtInfo)jwtData);
        }
    }
}