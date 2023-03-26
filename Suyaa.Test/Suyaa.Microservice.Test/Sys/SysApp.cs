using Microsoft.AspNetCore.Mvc;
using Suyaa.Microservice.Dependency;
using Suyaa.Microservice.Exceptions;
using Suyaa.Microservice.Results;
using Suyaa.Microservice.Test.Sys.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Suyaa.Microservice.Test.Sys
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
        /// <exception cref="FriendlyException"></exception>
        [Get]
        public void Check()
        {
            throw new FriendlyException($"尚未实现的接口");
        }

        /// <summary>
        /// 异步检测
        /// </summary>
        /// <exception cref="FriendlyException"></exception>
        [Get]
        public async Task Check2Async()
        {
            throw new FriendlyException($"尚未实现的接口");
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
