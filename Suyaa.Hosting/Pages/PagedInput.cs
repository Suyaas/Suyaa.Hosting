using System.ComponentModel.DataAnnotations;
using Suyaa.Hosting.Dependency;

namespace Suyaa.Hosting.Pages
{
    /// <summary>
    /// 分页输出
    /// </summary>
    public class PagedInput : IPagedInput
    {
        /// <summary>
        /// 分页小大
        /// </summary>
        [Range(1, 65535)]
        public int PageSize { get; set; } = 65535;
        /// <summary>
        /// 页码
        /// </summary>
        public int Page { get; set; } = 1;
    }
}
