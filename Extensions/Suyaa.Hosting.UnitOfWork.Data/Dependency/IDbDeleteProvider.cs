using Suyaa.Data.Dependency;

namespace Suyaa.Hosting.Data.Dependency
{
    /// <summary>
    /// 数据删除供应商
    /// </summary>
    public interface IDbDeleteProvider<TEntity, TId> : IDbDeletable<TEntity, TId>
        where TEntity : class, IEntity<TId>
        where TId : notnull
    {

    }
}
