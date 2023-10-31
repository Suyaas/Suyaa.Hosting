using Suyaa.Hosting.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlServerDemo.Entities
{
    [Table("SystemTables", Schema = "dbo")]
    public class SystemTables : Entity<decimal>
    {
        public SystemTables() : base(0)
        {
        }

        /// <summary>
        /// Id
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override decimal Id { get => base.Id; set => base.Id = value; }

        /// <summary>
        /// Name
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// 显示名称
        /// </summary>
        public string Text { get; set; } = string.Empty;
        /// <summary>
        /// 对象Id
        /// </summary>
        public decimal ObjectID { get; set; }
    }
}
