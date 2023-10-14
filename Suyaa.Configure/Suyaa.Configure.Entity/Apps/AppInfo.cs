using Suyaa.Data.Attributes;
using Suyaa.Data.Enums;
using Suyaa.Hosting.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Suyaa.Configure.Entity.Apps
{
    /// <summary>
    /// 应用信息
    /// </summary>
    [DbTable(Convert = DbNameConvertTypes.UnderlineLower)]
    public class AppInfo : GuidKeyEntity
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

        /// <summary>
        /// 项目Id
        /// </summary>
        [Description("项目Id")]
        [StringLength(50)]
        [DbColumn(Convert = DbNameConvertTypes.UnderlineLower)]
        [DbColumnType(DbColumnTypes.Varchar, 50)]
        public virtual string ProjectId { get; set; } = string.Empty;

        /// <summary>
        /// 应用Id
        /// </summary>
        [Description("应用Id")]
        [StringLength(128)]
        [DbColumn(Convert = DbNameConvertTypes.UnderlineLower)]
        [DbColumnType(DbColumnTypes.Varchar, 128)]
        public virtual string AppId { get; set; } = string.Empty;

        /// <summary>
        /// 应用密钥
        /// </summary>
        [Description("应用密钥")]
        [StringLength(128)]
        [DbColumn(Convert = DbNameConvertTypes.UnderlineLower)]
        [DbColumnType(DbColumnTypes.Varchar, 128)]
        public virtual string AppKey { get; set; } = string.Empty;
    }
}