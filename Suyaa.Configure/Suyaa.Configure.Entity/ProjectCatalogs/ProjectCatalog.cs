﻿using Suyaa.Data.Attributes;
using Suyaa.Data.Enums;
using Suyaa.Hosting.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Suyaa.Configure.Entity.ProjectCatalogs
{
    /// <summary>
    /// 项目
    /// </summary>
    [DbTable(Convert = DbNameConvertTypes.UnderlineLower)]
    public class ProjectCatalog : GuidKeyEntity
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