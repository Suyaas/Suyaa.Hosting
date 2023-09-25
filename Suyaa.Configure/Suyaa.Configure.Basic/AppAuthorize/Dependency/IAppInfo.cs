using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Configure.Basic.AppAuthorize.Dependency
{
    /// <summary>
    /// 应用信息
    /// </summary>
    public interface IAppInfo
    {
        /// <summary>
        /// 应用Id
        /// </summary>
        string AppId { get; set; }
        /// <summary>
        /// 应用密钥
        /// </summary>
        string AppKey { get; set; }
    }
}
