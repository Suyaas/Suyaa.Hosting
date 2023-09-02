using Suyaa.Configure.Cores.Users;
using Suyaa.Configure.Cores.Users.Dto;
using Suyaa.Configure.Cores.Users.Sto;
using Suyaa.Configure.Entity.Projects;
using Suyaa.Data;
using Suyaa.Hosting.Attributes;
using Suyaa.Hosting.Services;

namespace Suyaa.Configure.App.Users
{
    /// <summary>
    /// 用户
    /// </summary>
    [App("User")]
    public class UserApp : ServiceApp
    {
        private readonly IUserCore _userCore;
        private readonly IServiceProvider _provider;

        /// <summary>
        /// 用户
        /// </summary>
        public UserApp(
            IUserCore userCore,
            IServiceProvider provider
            )
        {
            _userCore = userCore;
            _provider = provider;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        [Put]
        public async Task<UserLoginOutput> Login(UserLoginInput input)
        {
            return await _userCore.Login(input);
        }
    }
}