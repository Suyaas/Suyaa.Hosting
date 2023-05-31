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
