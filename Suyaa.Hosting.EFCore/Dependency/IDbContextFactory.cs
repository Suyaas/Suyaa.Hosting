using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Hosting.EFCore.Dependency
{
    /// <summary>
    /// 数据库上下文工厂
    /// </summary>
    public interface IDbContextFactory
    {
        /// <summary>
        /// 获取实例
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        DbEntityDescriptor? GetEntity(Type type);

        /// <summary>
        /// 获取实例
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        DbEntityDescriptor? GetEntity<TEntity>();
    }
}
