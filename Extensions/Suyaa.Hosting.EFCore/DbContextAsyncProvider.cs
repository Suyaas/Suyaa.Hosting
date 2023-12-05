using Suyaa.Hosting.Common.DependencyManager.Dependency;
using Suyaa.Hosting.EFCore.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Hosting.EFCore
{
    /// <summary>
    /// 数据库上下文异步工作
    /// </summary>
    public class DbContextAsyncProvider : IDbContextAsyncProvider
    {

        // 缓存操作异步对象
        private static readonly AsyncLocal<DbContentWrapper> _dbContentAsyncLocal = new AsyncLocal<DbContentWrapper>();

        #region DI注入

        private readonly IDependencyManager _dependencyManager;

        /// <summary>
        /// 数据库上下文异步工作
        /// </summary>
        public DbContextAsyncProvider(
            IDependencyManager dependencyManager
            )
        {
            _dependencyManager = dependencyManager;
        }

        #endregion

        /// <summary>
        /// 获取当前工作
        /// </summary>
        /// <returns></returns>
        public IDbContextAsyncWork? GetCurrentWork()
        {
            //if (_dbContentAsyncLocal.Value is null)
            //{
            //    var work = _dependencyManager.Resolve<IDbContextAsyncWork>();
            //    SetCurrentWork(work);
            //    return work;
            //}
            //if (_dbContentAsyncLocal.Value.DbContextAsyncWork is null)
            //{
            //    var work = _dependencyManager.Resolve<IDbContextAsyncWork>();
            //    SetCurrentWork(work);
            //    return work;
            //}
            if(_dbContentAsyncLocal.Value is null) return null;
            return _dbContentAsyncLocal.Value.DbContextAsyncWork;
        }

        /// <summary>
        /// 设置当前工作
        /// </summary>
        /// <param name="work"></param>
        public void SetCurrentWork(IDbContextAsyncWork? work)
        {
            lock (_dbContentAsyncLocal)
            {
                if (_dbContentAsyncLocal.Value is null)
                {
                    _dbContentAsyncLocal.Value = new DbContentWrapper(work);
                    return;
                }
                _dbContentAsyncLocal.Value.DbContextAsyncWork?.Dispose();
                _dbContentAsyncLocal.Value = new DbContentWrapper(null);
            }
        }
    }
}
