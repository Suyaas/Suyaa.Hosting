using Suyaa.Data.Dependency;

namespace Suyaa.Hosting.Data.Dependency
{
    /// <summary>
    /// 可查询
    /// </summary>
    public interface IDbQueryable<TEntity, TId>
        where TEntity : class, IEntity<TId>
        where TId : notnull
    {
        /// <summary>
        /// 获取查询
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> Query();
    }
}
