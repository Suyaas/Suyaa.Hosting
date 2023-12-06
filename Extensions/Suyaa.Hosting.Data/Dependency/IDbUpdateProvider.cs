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
    /// 数据更新供应商
    /// </summary>
    public interface IDbUpdateProvider<TEntity, TId>: IDbUpdatable<TEntity, TId>
        where TEntity : class, IEntity<TId>
        where TId : notnull
    {
    }
}
