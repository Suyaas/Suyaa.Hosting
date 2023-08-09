using Microsoft.AspNetCore.Mvc;

namespace Suyaa.Hosting.Controllers
{
    /// <summary>
    /// 测试
    /// </summary>
    [ApiController]
    public class TestController : ControllerBase
    {
        /// <summary>
        /// GetInfo
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Api/Test/GetInfo")]
        public string GetInfo() { return "OK"; }
    }
}
