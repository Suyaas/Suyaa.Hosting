using Suyaa.DependencyInjection;
using Suyaa.Hosting.EFCore.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Hosting.EFCore
{
    /// <summary>
    /// 数据库上下文异步管理器
    /// </summary>
    public sealed class DbContextAsyncManager : IDbContextAsyncManager
    {
        #region DI注入

        private readonly IDependencyManager _dependencyManager;
        private readonly IDbContextAsyncProvider _dbContextAsyncProvider;

        /// <summary>
        /// 数据库上下文异步管理器
        /// </summary>
        public DbContextAsyncManager(
            IDependencyManager dependencyManager,
            IDbContextAsyncProvider dbContextAsyncProvider
            )
        {
            _dependencyManager = dependencyManager;
            _dbContextAsyncProvider = dbContextAsyncProvider;
        }

        #endregion

        /// <summary>
        /// 创建一个作业
        /// </summary>
        /// <returns></returns>
        public IDbContextAsyncWork CreateWork()
        {
            var work = _dependencyManager.Resolve<IDbContextAsyncWork>();
            _dbContextAsyncProvider.SetCurrentWork(work);
            return work;
        }
    }
}
