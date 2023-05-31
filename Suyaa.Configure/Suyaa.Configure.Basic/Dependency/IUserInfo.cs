using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Configure.Basic.Dependency
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public interface IUserInfo
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        long UserId { get; set; }
    }
}
