using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Microservice.Test.Sys.Dto
{
    /// <summary>
    /// 系统信息
    /// </summary>
    public class SysVersion
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 版本
        /// </summary>
        public string Version { get; set; } = string.Empty;
    }
}
