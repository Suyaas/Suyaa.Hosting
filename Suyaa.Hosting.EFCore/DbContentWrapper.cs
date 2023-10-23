using Suyaa.Data;
using Suyaa.EFCore;
using Suyaa.EFCore.Dependency;
using Suyaa.Hosting.EFCore.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Hosting.EFCore
{
    /// <summary>
    /// 数据库上下文包裹层
    /// </summary>
    public sealed class DbContentWrapper
    {

        /// <summary>
        /// 数据库上下文包裹层
        /// </summary>
        public DbContentWrapper(IDbContextAsyncWork? dbContextAsyncWork)
        {
            DbContextAsyncWork = dbContextAsyncWork;

            //DbContext = dbContext;
            //dbContext.ConnectionDescriptor.ToConnectionString();
        }

        /// <summary>
        /// 数据库上下文异步工作
        /// </summary>
        public IDbContextAsyncWork? DbContextAsyncWork { get; }
    }
}
