using SqlServerDemo.Entities;
using Suyaa.Hosting.AutoMapper.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlServerDemo.Cores.Dto
{
    [MapFrom(typeof(SystemObjects))]
    public class SystemObjectsDto
    {
        /// <summary>
        /// Name
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Version
        /// </summary>
        public string? Version { get; set; }
        /// <summary>
        /// Version
        /// </summary>
        public string? MiniVersion { get; set; }
    }
}
