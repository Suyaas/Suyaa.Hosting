using Suyaa.Data.Dependency;

namespace Suyaa.Hosting.Data.Dependency
{
    /// <summary>
    /// 可删除
    /// </summary>
    public interface IDbDeletable<TEntity, TId>
        where TEntity : class, IEntity<TId>
        where TId : notnull
    {
        /// <summary>
        /// 数据更新
        /// </summary>
        /// <returns></returns>
        void Delete(TEntity entity);
        /// <summary>
        /// 数据更新
        /// </summary>
        /// <returns></returns>
        Task DeleteAsync(TEntity entity);
    }
}
