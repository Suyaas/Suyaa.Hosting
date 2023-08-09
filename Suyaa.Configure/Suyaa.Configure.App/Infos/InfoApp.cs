using Microsoft.AspNetCore.Mvc;
using Suyaa.Hosting.Attributes;
using Suyaa.Hosting.Dependency;
using Suyaa.Hosting.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Configure.Apps.Infos
{
    /// <summary>
    /// 信息
    /// </summary>
    public sealed class InfoApp : ServiceApp
    {
        /// <summary>
        /// 获取版本号
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string GetVersion()
        {
            return sy.Assembly.Version;
        }
    }
}
