using System.ComponentModel.DataAnnotations;
using Suyaa.Configure.Entity.Projects;
using Suyaa.Hosting.AutoMapper.Attributes;

namespace Suyaa.Configure.Cores.Projects.Dto
{
    /// <summary>
    /// 项目添加
    /// </summary>
    [MapTo(typeof(Project))]
    public class ProjectInsertInput
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
