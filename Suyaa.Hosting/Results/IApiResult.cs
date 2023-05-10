using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace Suyaa.Hosting.Results
{
    /// <summary>
    /// Api执行结果
    /// </summary>
    public interface IApiResult : IActionResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        bool Success { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        string? Message { get; set; }
    }
}
