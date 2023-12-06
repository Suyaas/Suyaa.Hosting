namespace Suyaa.Hosting.Data.Dependency
{
    /// <summary>
    /// 拥有更新信息
    /// </summary>
    public interface IHaveModification
    {
        /// <summary>
        /// 最后更新时间
        /// </summary>
        DateTime LastModificationTime { get; set; }
    }
}
