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
    public class SystemObjects : Entity<long>
    {
        public SystemObjects() : base(0)
        {
        }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Version
        /// </summary>
        public string Version { get; set; } = string.Empty;
    }
}
