using Suyaa.Data.Dependency;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Suyaa.Hosting.Dependency;
using Suyaa.Configure.Entity.Projects;
using Suyaa.Hosting.Attributes;
using Suyaa.Configure.Entity.ProjectCatalogs;

namespace Suyaa.Configure.Cores.ProjectCatalogs.Dto
{
    /// <summary>
    /// 项目添加
    /// </summary>
    [MapTo(typeof(ProjectCatalog))]
    public class ProjectCatalogInsertInput
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; } = string.Empty;
    }
}
