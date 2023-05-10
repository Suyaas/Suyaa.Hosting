using Suyaa.Data.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Suyaa.Configure.Entity.Projects
{
    public class Project : GuidKeyEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Description("名称")]
        [Column("name")]
        [StringLength(128)]
        public virtual string Name { get; set; } = string.Empty;

        /// <summary>
        /// 描述
        /// </summary>
        [Description("描述")]
        [Column("description")]
        [StringLength(128)]
        public virtual string Description { get; set; } = string.Empty;
    }
}