using Suyaa.Data.Dependency;

namespace Suyaa.Hosting.Data.Dependency
{
    /// <summary>
    /// 可新增
    /// </summary>
    public interface IDbInsertable<TEntity, TId>
        where TEntity : class, IEntity<TId>
        where TId : notnull
    {
        /// <summary>
        /// 数据插入
        /// </summary>
        /// <returns></returns>
        Task InsertAsync(TEntity entity);
    }
}
