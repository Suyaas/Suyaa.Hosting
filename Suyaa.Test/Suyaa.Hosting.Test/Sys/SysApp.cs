using Microsoft.AspNetCore.Mvc;
using Suyaa.Hosting.Dependency;
using Suyaa.Hosting.Results;
using Suyaa.Hosting.Test.Sys.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Suyaa.Hosting.Test.Sys
{
    /// <summary>
    /// 系统
    /// </summary>
    [App("Sys")]
    public class SysApp : ServiceApp
    {
        private readonly ISysCore _sysCore;

        /// <summary>
        /// 系统
        /// </summary>
        public SysApp(
            ISysCore sysCore
            )
        {
            _sysCore = sysCore;
        }

        /// <summary>
        /// 获取系统版本
        /// </summary>
        /// <returns></returns>
        [Get]
        public ApiResult<SysVersion> GetVersion()
        {
            return _sysCore.GetVersionInfo();
        }

        /// <summary>
        /// 获取名称
        /// </summary>
        /// <returns></returns>
        [Get]
        public string GetName()
        {
            return _sysCore.GetVersionInfo().Name;
        }

        /// <summary>
        /// 检测
        /// </summary>
        /// <exception cref="HostFriendlyException"></exception>
        [Get]
        public void Check()
        {
            throw new HostFriendlyException($"尚未实现的接口");
        }

        /// <summary>
        /// 异步检测
        /// </summary>
        /// <exception cref="HostFriendlyException"></exception>
        [Get]
        public async Task Check2Async()
        {
            await Task.Delay(1);
            throw new HostFriendlyException($"尚未实现的接口");
        }

        /// <summary>
        /// 获取系统信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetVersionAsync")]
        public async Task<SysVersion> GetVersionAsync()
        {
            await Task.CompletedTask;
            throw new Exception($"尚未实现的接口");
        }
    }
}
