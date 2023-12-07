using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Suyaa.Hosting.Infrastructure.Results.Dependency;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Suyaa.Hosting.Infrastructure.Results
{
    /// <summary>
    /// API输出结果
    /// </summary>
    public class ApiResult : IApiResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        [JsonPropertyName("success")]
        public virtual bool Success { get; set; }

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
            Success = true;
        }

        /// <summary>
        /// 快速赋值结果
        /// </summary>
        /// <param name="value">值类型</param>
        public static implicit operator ApiResult(bool value)
        {
            return new ApiResult() { Success = value };
        }

        /// <summary>
        /// 执行结果
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected virtual async Task OnExecuteResultAsync(HttpContext context)
        {
            var json = JsonSerializer.Serialize(this);
            // 清理输出状态
            context.Response.Clear();
            context.Response.ContentType = "application/json";
            await context.Response.Body.WriteAsync(Encoding.UTF8.GetBytes(json));
        }

        /// <summary>
        /// 执行结果
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task ExecuteResultAsync(HttpContext context)
        {
            await OnExecuteResultAsync(context);
        }

        /// <summary>
        /// 执行结果
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task ExecuteResultAsync(ActionContext context)
        {
            await OnExecuteResultAsync(context.HttpContext);
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
        /// 执行结果
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected override async Task OnExecuteResultAsync(HttpContext context)
        {
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
            var json = JsonSerializer.Serialize(this, options);
            // 清理输出状态
            context.Response.Clear();
            context.Response.ContentType = "application/json";
            await context.Response.Body.WriteAsync(Encoding.UTF8.GetBytes(json));
        }

        /// <summary>
        /// 快速赋值结果
        /// </summary>
        /// <param name="value">值类型</param>
        public static implicit operator ApiResult<T>(T? value)
        {
            return new ApiResult<T>() { Success = true, Data = value };
        }
    }
}
