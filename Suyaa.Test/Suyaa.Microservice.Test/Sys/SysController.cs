using Microsoft.AspNetCore.Mvc;
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
    [Route("api/[controller]")]
    [ApiController]
    public class SysController : ControllerBase
    {
        private readonly ISysCore _sysCore;

        /// <summary>
        /// 系统
        /// </summary>
        public SysController(
            ISysCore sysCore
            )
        {
            _sysCore = sysCore;
        }

        [HttpGet("GetVersion")]
        public ApiResult<SysVersion> GetVersion()
        {
            return _sysCore.GetVersionInfo();
        }

        [HttpGet("GetName")]
        public string GetName()
        {
            return _sysCore.GetVersionInfo().Name;
        }

        [HttpGet("Check")]
        public void Check()
        {
        }

        [HttpGet("GetVersionAsync")]
        public async Task<SysVersion> GetVersionAsync()
        {
            await Task.Delay(1);
            return _sysCore.GetVersionInfo();
        }
    }
}
