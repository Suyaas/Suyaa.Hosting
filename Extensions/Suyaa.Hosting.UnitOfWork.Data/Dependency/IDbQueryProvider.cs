using Suyaa.Data.Dependency;

namespace Suyaa.Hosting.Data.Dependency
{
    /// <summary>
    /// 查询供应商
    /// </summary>
    public interface IDbQueryProvider<TEntity, TId> : IDbQueryable<TEntity, TId>
        where TEntity : class, IEntity<TId>
        where TId : notnull
    {

    }
}
