using Suyaa.Hosting.Dependency;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Configure.Cores.Projects.Sto
{
    /// <summary>
    /// 项目列表入参
    /// </summary>
    public class ProjectListInput : IPagedInput
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
