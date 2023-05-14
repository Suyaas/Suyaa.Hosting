using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Configure.Cores.Users.Sto
{
    /// <summary>
    /// 用户登录入参
    /// </summary>
    public class UserLoginInput
    {
        /// <summary>
        /// 账号
        /// </summary>
        public virtual string Account { get; set; } = string.Empty;
        /// <summary>
        /// 密码
        /// </summary>
        public virtual string Password { get; set; } = string.Empty;
    }
}
