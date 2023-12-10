using Suyaa.Data;
using Suyaa.Data.Descriptors;
using Suyaa.Hosting.UnitOfWork.EFCore.Descriptors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Hosting.UnitOfWork.EFCore.Dependency
{
    /// <summary>
    /// 数据库上下文工厂
    /// </summary>
    public interface IDbSetFactory
    {
        /// <summary>
        /// 获取实例
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        DbSetDescriptor? GetDbSet(Type type);

        /// <summary>
        /// 获取实例
        /// </summary>
        /// <returns></returns>
        DbSetDescriptor? GetDbSet<TEntity>();

        /// <summary>
        /// 获取实例
        /// </summary>
        /// <param name="dbConnectionDescriptorName"></param>
        /// <returns></returns>
        IEnumerable<DbSetDescriptor> GetDbSets(string dbConnectionDescriptorName);
    }
}
