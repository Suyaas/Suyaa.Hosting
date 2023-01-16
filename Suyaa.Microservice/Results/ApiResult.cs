using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Suyaa.Microservice.Results
{
    /// <summary>
    /// API输出结果
    /// </summary>
    public class ApiResult : IApiResult, IActionResult
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

        /// <summary>
        /// 执行结果
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected virtual async Task OnExecuteResultAsync(ActionContext context)
        {
            var json = JsonSerializer.Serialize(this);
            await context.HttpContext.Response.Body.WriteAsync(Encoding.UTF8.GetBytes(json));
        }

        /// <summary>
        /// 执行结果
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task ExecuteResultAsync(ActionContext context)
        {
            await OnExecuteResultAsync(context);
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
        /// 结果类型
        /// </summary>
        [JsonPropertyName("dataType")]
        public virtual string? DataType { get; set; }

        /// <summary>
        /// API输出结果
        /// </summary>
        public ApiResult()
        {
            this.DataType = typeof(T).Name;
        }

        /// <summary>
        /// 执行结果
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected override async Task OnExecuteResultAsync(ActionContext context)
        {
            var json = JsonSerializer.Serialize(this);
            await context.HttpContext.Response.Body.WriteAsync(Encoding.UTF8.GetBytes(json));
        }

        /// <summary>
        /// 快速赋值结果
        /// </summary>
        /// <param name="value">值类型</param>
        public static implicit operator ApiResult<T>(T? value)
        {
            return new ApiResult<T>() { IsSuccess = true, Data = value };
        }
    }
}
