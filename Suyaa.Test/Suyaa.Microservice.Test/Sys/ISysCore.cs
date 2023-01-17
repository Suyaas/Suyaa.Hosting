using Suyaa.Microservice.Dependency;
using Suyaa.Microservice.Test.Sys.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Microservice.Test.Sys
{
    /// <summary>
    /// 系统核心服务
    /// </summary>
    public interface ISysCore : IServiceCore
    {
        /// <summary>
        /// 获取版本信息
        /// </summary>
        /// <returns></returns>
        SysVersion GetVersionInfo();
    }
}
