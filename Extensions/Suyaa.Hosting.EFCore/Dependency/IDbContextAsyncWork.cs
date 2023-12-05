using Suyaa.Data;
using Suyaa.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Hosting.EFCore.Dependency
{
    /// <summary>
    /// 数据库上下文异步工作
    /// </summary>
    public interface IDbContextAsyncWork : IDisposable
    {
        /// <summary>
        /// 完成
        /// </summary>
        /// <returns></returns>
        Task CompleteAsync();

        /// <summary>
        /// 是否完成
        /// </summary>
        bool IsCompleted { get; }

        /// <summary>
        /// 获取数据库上下文
        /// </summary>
        /// <param name="descriptor"></param>
        /// <returns></returns>
        DbDescriptorContext? GetDbContext(DbConnectionDescriptor descriptor);

        /// <summary>
        /// 设置数据库上下文
        /// </summary>
        /// <param name="dbContext"></param>
        void SetDbContext(DbDescriptorContext dbContext);
    }
}
