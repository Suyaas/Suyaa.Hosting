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
    public interface IUnitOfWorkProvider
    {
        /// <summary>
        /// 创建
        /// </summary>
        void OnCreate();

        /// <summary>
        /// 完成
        /// </summary>
        void OnComplete();

        /// <summary>
        /// 异步方式完成
        /// </summary>
        /// <returns></returns>
        Task OnCompleteAsync();
    }
}
