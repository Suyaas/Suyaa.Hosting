using Suyaa.DependencyInjection;
using Suyaa.Hosting.Data.Dependency;
using Suyaa.Hosting.Kernel;
using Suyaa.Hosting.Kernel.Dependency;

namespace Suyaa.Hosting.Data
{
    /// <summary>
    /// 数据仓库
    /// </summary>
    /// <typeparam name="TClass"></typeparam>
    /// <typeparam name="TId"></typeparam>
    public class Repository<TEntity, TId> : IRepository<TEntity, TId>
        where TEntity : class, IEntity<TId>
        where TId : notnull
    {

        #region DI注入

        private readonly IDependencyManager _dependencyManager;
        private readonly IQueryProvider<TEntity, TId> _queryProvider;
        private readonly IInsertProvider<TEntity, TId> _insertProvider;
        private readonly IUpdateProvider<TEntity, TId> _updateProvider;
        private readonly IDeleteProvider<TEntity, TId> _deleteProvider;

        /// <summary>
        /// 数据仓库
        /// </summary>
        public Repository(
            IDependencyManager dependencyManager,
            IQueryProvider<TEntity, TId> queryProvider,
            IInsertProvider<TEntity, TId> insertProvider,
            IUpdateProvider<TEntity, TId> updateProvider,
            IDeleteProvider<TEntity, TId> deleteProvider
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
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public IQueryable<TEntity> Query()
        {
            return _queryProvider.Query();
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task InsertAsync(TEntity entity)
        {
            await _insertProvider.InsertAsync(entity);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateAsync(TEntity entity)
        {
            await _updateProvider.UpdateAsync(entity);
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task DeleteAsync(TEntity entity)
        {
            await _deleteProvider.DeleteAsync(entity);
        }
    }
}