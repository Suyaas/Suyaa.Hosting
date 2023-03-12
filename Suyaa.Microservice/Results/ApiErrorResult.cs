using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
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
            this.Success = false;
        }

        /// <summary>
        /// 执行结果
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected override async Task OnExecuteResultAsync(HttpContext context)
        {
            var json = JsonSerializer.Serialize(this);
            // 清理输出状态
            context.Response.Clear();
            context.Response.ContentType = "application/json";
            await context.Response.Body.WriteAsync(Encoding.UTF8.GetBytes(json));
        }
    }
}
