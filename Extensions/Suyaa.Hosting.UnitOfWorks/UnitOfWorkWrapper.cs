using Suyaa.Hosting.UnitOfWork.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Hosting.UnitOfWork
{
    /// <summary>
    /// 工作单元包裹层
    /// </summary>
    public sealed class UnitOfWorkWrapper
    {
        /// <summary>
        /// 工作单元包裹层
        /// </summary>
        public UnitOfWorkWrapper()
        {
        }

        /// <summary>
        /// 工作单元包裹层
        /// </summary>
        /// <param name="unitOfWork"></param>
        public UnitOfWorkWrapper(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        /// <summary>
        /// 工作单元
        /// </summary>
        public IUnitOfWork? UnitOfWork { get; }
    }
}
