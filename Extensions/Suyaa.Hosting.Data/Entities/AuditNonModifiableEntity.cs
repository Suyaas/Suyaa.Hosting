using Suyaa.Data.Attributes;
using Suyaa.Data.Entities;
using Suyaa.Data.Enums;
using Suyaa.Hosting.Data.Dependency;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Hosting.Data.Entities
{
    /// <summary>
    /// 带审计的不可修改实体
    /// </summary>
    public abstract class AuditNonModifiableEntity : NonModifiableEntity, IHaveAuditCreation
    {
        /// <summary>
        /// 创建用户Id
        /// </summary>
        [Column]
        [ColumnType(ColumnValueType.Varchar, 36)]
        [StringLength(36)]
        [Description("创建用户Id")]
        public string CreationUserId { get; set; } = string.Empty;
    }
}
