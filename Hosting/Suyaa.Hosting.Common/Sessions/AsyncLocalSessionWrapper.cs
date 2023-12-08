using Suyaa.Hosting.Common.Sessions.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Hosting.Common.Sessions
{
    /// <summary>
    /// 交互信息包裹层
    /// </summary>
    public sealed class AsyncLocalSessionWrapper
    {
        /// <summary>
        /// 交互信息包裹层
        /// </summary>
        /// <param name="session"></param>
        public AsyncLocalSessionWrapper(ISession session)
        {
            Session = session;
        }

        /// <summary>
        /// 交互信息包裹层
        /// </summary>
        public AsyncLocalSessionWrapper() { }

        /// <summary>
        /// 交互信息
        /// </summary>
        public ISession? Session { get; }
    }
}
