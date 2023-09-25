using Suyaa.Configure.Cores.Users.Dto;
using Suyaa.Configure.Cores.Users.Sto;
using Suyaa.Hosting;
using Suyaa.Hosting.Kernel.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Configure.Cores.Users
{
    /// <summary>
    /// 用户
    /// </summary>
    public interface IUserCore : IServiceCore
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<UserLoginOutput> Login(UserLoginInput input);
    }
}
