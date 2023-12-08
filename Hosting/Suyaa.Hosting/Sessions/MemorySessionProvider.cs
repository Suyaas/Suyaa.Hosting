using Suyaa.Hosting.Common.Sessions.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Hosting.Sessions
{
    /// <summary>
    /// 内存交互信息供应商
    /// </summary>
    public sealed class MemorySessionProvider : ISessionProvider
    {
        /// <summary>
        /// 获取交互信息
        /// </summary>
        /// <returns></returns>
        public ISession GetSession()
        {
            return new MemorySession();
        }
    }
}
