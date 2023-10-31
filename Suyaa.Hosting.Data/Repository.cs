using Suyaa.DependencyInjection;
using Suyaa.Hosting.Data.Dependency;
using Suyaa.Hosting.Kernel;
using Suyaa.Hosting.Kernel.Dependency;

namespace Suyaa.Hosting.Data
{
    /// <summary>
    /// 数据仓库
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TId"></typeparam>
    public class Repository<TEntity, TId> : IRepository<TEntity, TId>
        where TEntity : class, IEntity<TId>
        where TId : notnull
    {

        #region DI注入

        private readonly IDependencyManager _dependencyManager;
        private readonly IDbQueryProvider<TEntity, TId> _queryProvider;
        private readonly IInsertProvider<TEntity, TId> _insertProvider;
        private readonly IDbUpdateProvider<TEntity, TId> _updateProvider;
        private readonly IDbDeleteProvider<TEntity, TId> _deleteProvider;

        /// <summary>
        /// 数据仓库
        /// </summary>
        public Repository(
            IDependencyManager dependencyManager,
            IDbQueryProvider<TEntity, TId> queryProvider,
            IInsertProvider<TEntity, TId> insertProvider,
            IDbUpdateProvider<TEntity, TId> updateProvider,
            IDbDeleteProvider<TEntity, TId> deleteProvider
            )
        {
            _dependencyManager = dependencyManager;
            _queryProvider = queryProvider;
            _insertProvider = insertProvider;
            _updateProvider = updateProvider;
            _deleteProvider = deleteProvider;
        }

        #endregion

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> Query()
        {
            return _queryProvider.Query();
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task InsertAsync(TEntity entity)
        {
            await _insertProvider.InsertAsync(entity);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateAsync(TEntity entity)
        {
            await _updateProvider.UpdateAsync(entity);
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task DeleteAsync(TEntity entity)
        {
            await _deleteProvider.DeleteAsync(entity);
        }
    }
}