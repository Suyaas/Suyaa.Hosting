using Suyaa.Configure.Basic.Configures;
using Suyaa.Configure.Cores.Users.Sto;
using Suyaa.Hosting.Dependency;
using Suyaa.Configure.Cores.Users.Dto;
using Suyaa.Hosting.Kernel;
using Suyaa.Hosting.Kernel.Dependency;
using Suyaa.Configure.Basic.Jwt;
using Suyaa.Hosting.Jwt.Dependency;
using Suyaa.Configure.Basic.Dependency;

namespace Suyaa.Configure.Cores.Users
{
    /// <summary>
    /// 用户
    /// </summary>
    public class UserCore : IUserCore
    {

        #region IOC注入

        private readonly UserConfigs _userConfigs;
        private readonly IMultilingualManager _i18n;
        private readonly IJwtManager _jwtManager;

        /// <summary>
        /// 用户
        /// </summary>
        public UserCore(
            IOptionConfig<UserConfigs> userConfigs,
            IMultilingualManager i18n,
            IJwtManager jwtManager
            )
        {
            _userConfigs = userConfigs.CurrentValue;
            _i18n = i18n;
            _jwtManager = jwtManager;
        }

        #endregion

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="HostFriendlyException"></exception>
        public async Task<UserLoginOutput> Login(UserLoginInput input)
        {
            var userConfig = _userConfigs.Where(d => d.Account == input.Account).FirstOrDefault();
            if (userConfig is null) throw new HostFriendlyException(_i18n.Content("Login fail."));
            if (userConfig.Password != input.Password) throw new HostFriendlyException(_i18n.Content("Login fail."));
            var token = _jwtManager.Provider.Builder.CreateToken(new JwtInfo() { Uid = userConfig.Id.ToString(), UserAccount = userConfig.Account });
            return await Task.FromResult(new UserLoginOutput()
            {
                Token = token.Token,
                RenewalTime = sy.Time.Now.AddHours(1).ToUnixTimeSeconds(),
            });
        }
    }
}
