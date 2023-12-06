using Suyaa.Data.Dependency;
using Suyaa.Hosting.Kernel.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Hosting.Data.Dependency
{
    /// <summary>
    /// 可删除
    /// </summary>
    public interface IDbDeletable<TEntity, TId>
        where TEntity : class, IEntity<TId>
        where TId : notnull
    {
        /// <summary>
        /// 数据更新
        /// </summary>
        /// <returns></returns>
        Task DeleteAsync(TEntity entity);
    }
}
