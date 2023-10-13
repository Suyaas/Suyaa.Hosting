using Suyaa.Hosting.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlServerDemo.Entities
{
    [Table("SystemObjects", Schema = "dbo")]
    public class SystemObjects : Entity<decimal>
    {
        public SystemObjects() : base(0)
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
        /// Version
        /// </summary>
        public string? Version { get; set; }
    }
}
