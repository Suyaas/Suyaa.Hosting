using Suyaa.Data.Dependency;
using Suyaa.Data.Dependency.Attributes;
using Suyaa.Data.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Suyaa.Configure.Entity.Projects
{
    /// <summary>
    /// 项目
    /// </summary>
    [DbTable(Convert = DbNameConvertTypes.UnderlineLower)]
    public class Project : GuidKeyEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Description("名称")]
        [StringLength(128)]
        [DbColumn(Convert = DbNameConvertTypes.UnderlineLower)]
        [DbColumnType(DbColumnTypes.Varchar, 128)]
        public virtual string Name { get; set; } = string.Empty;

        /// <summary>
        /// 描述
        /// </summary>
        [Description("描述")]
        [StringLength(128)]
        [DbColumn(Convert = DbNameConvertTypes.UnderlineLower)]
        [DbColumnType(DbColumnTypes.Varchar, 128)]
        public virtual string Description { get; set; } = string.Empty;
    }
}