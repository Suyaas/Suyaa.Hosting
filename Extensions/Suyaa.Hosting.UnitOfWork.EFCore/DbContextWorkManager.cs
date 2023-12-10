using Suyaa.Hosting.Common.DependencyInjection.Dependency;
using Suyaa.Hosting.Common.DependencyInjection.Helpers;
using Suyaa.Hosting.EFCore.Dependency;

namespace Suyaa.Hosting.EFCore
{
    /// <summary>
    /// 数据库上下文异步管理器
    /// </summary>
    public sealed class DbContextWorkManager : IDbContextWorkManager
    {

        // 缓存操作异步对象
        private static readonly AsyncLocal<DbContentWorkWrapper> _dbContentAsyncLocal = new AsyncLocal<DbContentWorkWrapper>();

        #region DI注入

        private readonly IDependencyManager _dependencyManager;
        private readonly IDbContextWorkProvider _dbContextAsyncProvider;

        /// <summary>
        /// 数据库上下文异步管理器
        /// </summary>
        public DbContextWorkManager(
            IDependencyManager dependencyManager,
            IDbContextWorkProvider dbContextAsyncProvider
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
        public IDbContextWork CreateWork()
        {
            // 释放之前的作业
            ReleaseWork();
            lock (_dbContentAsyncLocal)
            {
                var work = _dbContextAsyncProvider.CreateWork();
                _dbContentAsyncLocal.Value = new DbContentWorkWrapper(work);
                return work;
            }
        }

        /// <summary>
        /// 获取当前作业
        /// </summary>
        /// <returns></returns>
        public IDbContextWork? GetWork()
        {
            if (_dbContentAsyncLocal.Value is null) return null;
            return _dbContentAsyncLocal.Value.DbContextAsyncWork;
        }

        /// <summary>
        /// 释放当前作业
        /// </summary>
        public void ReleaseWork()
        {
            if (_dbContentAsyncLocal.Value is null) return;
            if (_dbContentAsyncLocal.Value.DbContextAsyncWork is null) return;
            var work = _dbContentAsyncLocal.Value.DbContextAsyncWork;
            lock (_dbContentAsyncLocal)
            {
                _dbContentAsyncLocal.Value = new DbContentWorkWrapper();
            }
            work.Dispose();
        }
    }
}
