using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Hosting.UnitOfWork.Dependency
{
    /// <summary>
    /// 工作单元管理器
    /// </summary>
    public interface IUnitOfWorkManager
    {
        /// <summary>
        /// 开始一个工作单元
        /// </summary>
        IUnitOfWork Begin();

        /// <summary>
        /// 获取当前工作单元
        /// </summary>
        IUnitOfWork? GetWork();

        /// <summary>
        /// 释放当前工作单元
        /// </summary>
        void ReleaseWork();
    }
}
