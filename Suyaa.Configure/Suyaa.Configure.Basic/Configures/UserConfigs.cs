using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Configure.Basic.Configures
{
    /// <summary>
    /// 用户配置
    /// </summary>
    public class UserConfigs : List<UserConfig>, IConfig
    {
        /// <summary>
        /// 默认配置
        /// </summary>
        public void Default()
        {

        }
    }

    /// <summary>
    /// 用户配置
    /// </summary>
    public class UserConfig
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public long Id { get; set; } = 0;
        /// <summary>
        /// 账户
        /// </summary>
        public string Account { get; set; } = string.Empty;
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; } = string.Empty;
    }
}
