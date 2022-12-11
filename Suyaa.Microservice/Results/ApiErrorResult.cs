using System.Text.Json.Serialization;

namespace Suyaa.Microservice.Results
{
    /// <summary>
    /// 错误结果
    /// </summary>
    public class ApiErrorResult : ApiResult
    {
        /// <summary>
        /// 错误代码
        /// </summary>
        [JsonPropertyName("errorCode")]
        public virtual int ErrorCode { get; set; }

        /// <summary>
        /// 对象实例化
        /// </summary>
        public ApiErrorResult()
        {
            this.IsSuccess = false;
        }
    }
}
