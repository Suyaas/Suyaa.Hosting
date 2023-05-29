using Suyaa.Configure.Cores.Users;
using Suyaa.Configure.Cores.Users.Dto;
using Suyaa.Configure.Cores.Users.Sto;
using Suyaa.Configure.Entity.Projects;
using Suyaa.Data;
using Suyaa.Hosting.Dependency;
using Suyaa.Hosting.Dependency.Attributes;

namespace Suyaa.Configure.App.Users
{
    /// <summary>
    /// 用户
    /// </summary>
    [App("User")]
    public class UserApp : ServiceApp
    {
        private readonly IUserCore _userCore;

        /// <summary>
        /// 用户
        /// </summary>
        public UserApp(
            IUserCore userCore
            )
        {
            _userCore = userCore;
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