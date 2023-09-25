namespace Suyaa.Hosting.Kernel.Dependency
{
    /// <summary>
    /// 数据实体
    /// </summary>
    /// <typeparam name="TId"></typeparam>
    public interface IEntity<TId>
        where TId : notnull
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        TId Id { get; set; }
    }
}
