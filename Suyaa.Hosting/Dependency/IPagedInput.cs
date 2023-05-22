namespace Suyaa.Hosting.Dependency
{
    /// <summary>
    /// 分页支持
    /// </summary>
    public interface IPagedInput
    {
        /// <summary>
        /// 分页大小
        /// </summary>
        int PageSize { get; }
        /// <summary>
        /// 页码
        /// </summary>
        int Page { get; }
    }
}
