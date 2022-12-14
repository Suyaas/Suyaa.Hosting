using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Suyaa.Microservice.Test
{
    /// <summary>
    /// 系统
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SysController : ControllerBase
    {
        [HttpGet("GetVersion")]
        public string GetVersion()
        {
            Dictionary<string, string> map = new Dictionary<string, string>();
            map["name"] = "Suyaa";
            map["version"] = "1.0.0";
            return JsonSerializer.Serialize(map);
        }
    }
}
