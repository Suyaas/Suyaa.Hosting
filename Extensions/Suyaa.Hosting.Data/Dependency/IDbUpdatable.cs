using Suyaa.Hosting.Kernel.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Hosting.Data.Dependency
{
    /// <summary>
    /// 可更新
    /// </summary>
    public interface IDbUpdatable<TEntity, TId>
        where TEntity : class, IEntity<TId>
        where TId : notnull
    {
        /// <summary>
        /// 数据更新
        /// </summary>
        /// <returns></returns>
        Task UpdateAsync(TEntity entity);
    }
}
