using Suyaa.Data.Dependency;

namespace Suyaa.Hosting.Data.Dependency
{
    /// <summary>
    /// 数据插入供应商
    /// </summary>
    public interface IDbInsertProvider<TEntity, TId> : IDbInsertable<TEntity, TId>
        where TEntity : class, IEntity<TId>
        where TId : notnull
    {
    }
}
