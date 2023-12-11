using Suyaa.Hosting.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleEfCoreHosting.Entities
{
    /// <summary>
    /// 测试
    /// </summary>
    [Table("test", Schema = "demo")]
    [Description("测试")]
    public sealed class Test : StandardEntity
    {
        /// <summary>
        /// 内容
        /// </summary>
        [Column]
        [Description("内容")]
        public string Content { get; set; } = string.Empty;
    }
}
