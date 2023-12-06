using Suyaa.Data.Attributes;
using Suyaa.Data.Entities;
using Suyaa.Data.Enums;
using Suyaa.Hosting.Data.Dependency;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Hosting.Data.Entities
{
    /// <summary>
    /// 不可修改实体
    /// </summary>
    public abstract class NonModifiableEntity : UUIDKeyEntity, IHaveCreation
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        [Column]
        [ColumnType(ColumnValueType.Datetime)]
        [Description("创建时间")]
        public DateTime CreationTime { get; set; } = DateTime.Now;
    }
}
