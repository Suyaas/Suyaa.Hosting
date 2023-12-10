using Suyaa.Hosting.UnitOfWork.EFCore.Descriptors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Hosting.UnitOfWork.EFCore.Dependency
{
    /// <summary>
    /// DbSet 供应商
    /// </summary>
    public interface IDbSetProvider
    {
        /// <summary>
        /// 获取 DbSet 描述集合
        /// </summary>
        /// <returns></returns>
        IEnumerable<DbSetDescriptor> GetDbSets();
    }
}
