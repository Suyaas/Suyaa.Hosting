using Suyaa.Configure.Basic.Configures;
using Suyaa.Configure.Cores.Users.Sto;
using Suyaa.Hosting.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Suyaa.Exceptions;
using Suyaa.Hosting;
using Suyaa.Configure.Cores.Users.Dto;
using System.Runtime;
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
        private readonly II18n _i18n;

        /// <summary>
        /// 用户
        /// </summary>
        public UserCore(
            IOptionConfig<UserConfigs> userConfigs,
            II18n i18n
            )
        {
            _userConfigs = userConfigs.CurrentValue;
            _i18n = i18n;
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
            var token = sy.Jwt.CreateToken(new JwtUserInfo() { UserId = userConfig.Id });
            return await Task.FromResult(new UserLoginOutput()
            {
                Token = token,
                RenewalTime = sy.Time.Now.AddHours(1).ToUnixTimeSeconds()
            });
        }
    }
}
