using Suyaa.Data.Attributes;
using Suyaa.Data.Enums;
using Suyaa.Hosting.Kernel.Dependency;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Hosting.Data.Entities
{
    /// <summary>
    /// 不可删除实体
    /// </summary>
    public abstract class NonDeletableEntity : NonModifiableEntity, IHaveModification
    {
        /// <summary>
        /// 最后更新时间
        /// </summary>
        [Column]
        [ColumnType(ColumnValueType.Datetime)]
        [Description("最后更新时间")]
        public DateTime LastModificationTime { get; set; } = DateTime.Now;
    }
}
