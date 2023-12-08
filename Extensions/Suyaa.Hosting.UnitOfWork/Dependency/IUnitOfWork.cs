using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Hosting.UnitOfWork.Dependency
{
    /// <summary>
    /// 工作单元
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// 完成
        /// </summary>
        void Complete();

        /// <summary>
        /// 异步方式完成
        /// </summary>
        /// <returns></returns>
        Task CompleteAsync();
    }
}
