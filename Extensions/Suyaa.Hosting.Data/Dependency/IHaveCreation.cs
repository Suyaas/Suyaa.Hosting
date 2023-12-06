namespace Suyaa.Hosting.Data.Dependency
{
    /// <summary>
    /// 拥有创建信息
    /// </summary>
    public interface IHaveCreation
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        DateTime CreationTime { get; set; }
    }
}
