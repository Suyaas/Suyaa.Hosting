using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Hosting.Common.Sessions.Dependency
{
    /// <summary>
    /// 交互信息管理器
    /// </summary>
    public interface ISessionManager
    {
        /// <summary>
        /// 获取当前交互信息
        /// </summary>
        /// <returns></returns>
        ISession GetSession();

        /// <summary>
        /// 释放当前交互信息
        /// </summary>
        /// <returns></returns>
        void ReleaseSession();
    }
}
