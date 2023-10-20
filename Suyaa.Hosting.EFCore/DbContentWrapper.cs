using Suyaa.EFCore;
using Suyaa.EFCore.Dependency;
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
        public DbContentWrapper(DbContextBase dbContext)
        {
            DbContext = dbContext;
        }

        /// <summary>
        /// 数据库上下文
        /// </summary>
        public DbContextBase DbContext { get; }
    }
}
