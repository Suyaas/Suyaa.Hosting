using System.ComponentModel.DataAnnotations;
using Suyaa.Configure.Entity.ProjectCatalogs;
using Suyaa.Hosting.AutoMapper.Attributes;

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
