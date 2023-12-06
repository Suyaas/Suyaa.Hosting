namespace Suyaa.Hosting.Data.Dependency
{
    /// <summary>
    /// 拥有更新信息
    /// </summary>
    public interface IHaveDeletion
    {
        /// <summary>
        /// 删除时间
        /// </summary>
        DateTime? DeletionTime { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        bool IsDelete { get; set; }
    }
}
