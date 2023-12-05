namespace Suyaa.Hosting.Dependency
{
    /// <summary>
    /// 分页支持
    /// </summary>
    public interface IPagedOutput
    {
        /// <summary>
        /// 总行数
        /// </summary>
        int RowCount { get; set; }
        /// <summary>
        /// 分页大小
        /// </summary>
        int PageSize { get; set; }
        /// <summary>
        /// 页码
        /// </summary>
        int Page { get; set; }
    }
}
