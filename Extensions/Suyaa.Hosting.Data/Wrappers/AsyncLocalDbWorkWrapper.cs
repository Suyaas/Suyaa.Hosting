using Suyaa.Data.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Hosting.Data.Wrappers
{
    /// <summary>
    /// 异步共享数据库作业包裹层
    /// </summary>
    public sealed class AsyncLocalDbWorkWrapper
    {
        /// <summary>
        /// 异步共享数据库作业包裹层
        /// </summary>
        public AsyncLocalDbWorkWrapper() { }

        /// <summary>
        /// 异步共享数据库作业包裹层
        /// </summary>
        public AsyncLocalDbWorkWrapper(IDbWork dbWork)
        {
            DbWork = dbWork;
        }

        /// <summary>
        /// 数据库作业
        /// </summary>
        public IDbWork? DbWork { get; }
    }
}
