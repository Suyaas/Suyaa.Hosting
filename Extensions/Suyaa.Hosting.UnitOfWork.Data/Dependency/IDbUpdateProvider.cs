using Suyaa.Data.Dependency;

namespace Suyaa.Hosting.Data.Dependency
{
    /// <summary>
    /// 数据更新供应商
    /// </summary>
    public interface IDbUpdateProvider<TEntity, TId> : IDbUpdatable<TEntity, TId>
        where TEntity : class, IEntity<TId>
        where TId : notnull
    {
    }
}
