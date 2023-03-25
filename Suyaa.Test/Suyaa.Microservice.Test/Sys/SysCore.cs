﻿using Suyaa.Microservice.Test.Sys.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Microservice.Test.Sys
{
    /// <summary>
    /// 系统
    /// </summary>
    public class SysCore : ISysCore
    {
        /// <summary>
        /// 获取版本信息
        /// </summary>
        /// <returns></returns>
        public SysVersion GetVersionInfo()
        {
            return new SysVersion()
            {
                Name = "Suyaa",
                Version = "1.0.0"
            };
        }
    }
}