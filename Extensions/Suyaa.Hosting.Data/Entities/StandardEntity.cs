using Suyaa.Data.Attributes;
using Suyaa.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Suyaa.Hosting.Data.Dependency;

namespace Suyaa.Hosting.Data.Entities
{
    /// <summary>
    /// 标准实体
    /// </summary>
    public abstract class StandardEntity : NonDeletableEntity, IHaveDeletion
    {
        /// <summary>
        /// 删除时间
        /// </summary>
        [Column]
        [ColumnType(ColumnValueType.Datetime)]
        [Description("删除时间")]
        public DateTime? DeletionTime { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        [Column]
        [Description("是否删除")]
        public bool IsDelete { get; set; }
    }
}
