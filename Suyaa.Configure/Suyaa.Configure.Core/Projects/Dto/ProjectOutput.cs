using Suyaa.Configure.Entity.Projects;
using Suyaa.Hosting.AutoMapper.Attributes;

namespace Suyaa.Configure.Cores.Projects.Dto
{
    /// <summary>
    /// 项目出参
    /// </summary>
    [MapFrom(typeof(Project))]
    public class ProjectOutput
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; } = string.Empty;
    }
}
