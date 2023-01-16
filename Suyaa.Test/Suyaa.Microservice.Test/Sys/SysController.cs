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
        [HttpGet("GetVersion")]
        public ApiResult<SysVersion> GetVersion()
        {
            return new SysVersion()
            {
                Name = "Suyaa",
                Version = "1.0.0"
            };
        }

        [HttpGet("GetName")]
        public string GetName()
        {
            return "Suyaa";
        }

        [HttpGet("Check")]
        public void Check()
        {
        }

        [HttpGet("CheckAsync")]
        public async Task CheckAsync()
        {
            await Task.CompletedTask;
        }
    }
}
