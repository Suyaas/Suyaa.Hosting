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
    public sealed class DbContentWorkWrapper
    {

        /// <summary>
        /// 数据库上下文包裹层
        /// </summary>
        public DbContentWorkWrapper()
        {
        }

        /// <summary>
        /// 数据库上下文包裹层
        /// </summary>
        public DbContentWorkWrapper(IDbContextWork? dbContextAsyncWork)
        {
            DbContextAsyncWork = dbContextAsyncWork;
        }

        /// <summary>
        /// 数据库上下文异步工作
        /// </summary>
        public IDbContextWork? DbContextAsyncWork { get; }
    }
}
