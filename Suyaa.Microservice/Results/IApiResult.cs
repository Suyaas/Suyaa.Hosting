using System.Text.Json.Serialization;

namespace Suyaa.Microservice.Results
{
    /// <summary>
    /// Api执行结果
    /// </summary>
    public interface IApiResult
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
