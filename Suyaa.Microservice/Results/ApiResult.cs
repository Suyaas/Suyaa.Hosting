using System.Text.Json.Serialization;

namespace Suyaa.Microservice.Results
{
    /// <summary>
    /// API输出结果
    /// </summary>
    public class ApiResult : IApiResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        [JsonPropertyName("isSuccess")]
        public virtual bool IsSuccess { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        [JsonPropertyName("message")]
        public virtual string? Message { get; set; }

        /// <summary>
        /// API输出结果
        /// </summary>
        public ApiResult()
        {
            this.IsSuccess = true;
        }

        /// <summary>
        /// 快速赋值结果
        /// </summary>
        /// <param name="value">值类型</param>
        public static implicit operator ApiResult(bool value)
        {
            return new ApiResult() { IsSuccess = value };
        }
    }

    /// <summary>
    /// API输出结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiResult<T> : ApiResult
    {
        /// <summary>
        /// 关联的结果
        /// </summary>
        [JsonPropertyName("data")]
        public virtual T? Data { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        [JsonPropertyName("dataType")]
        public virtual string? DataType { get; set; }
    }
}
