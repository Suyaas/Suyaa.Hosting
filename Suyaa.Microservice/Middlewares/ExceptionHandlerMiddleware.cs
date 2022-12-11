using System.Text.Json;
using System.Text;
using Suyaa.Microservice.Exceptions;
using Suyaa.Microservice.Results;

namespace Suyaa.Microservice.Middlewares
{
    /// <summary>
    /// 异常中间件
    /// </summary>
    public class ExceptionHandlerMiddleware
    {
        // 变量
        private RequestDelegate _next;

        /// <summary>
        /// 对象实例化
        /// </summary>
        /// <param name="next"></param>
        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                if (ex is InvalidOperationException) ex = ex.InnerException ?? new Exception();
                // 错误记录
                egg.Logger.Error(ex.ToString(), context.Request.Path);
                // 清理输出状态
                context.Response.Clear();
                context.Response.ContentType = "application/json";
                if (ex is SuyaaFriendlyException)
                {
                    SuyaaFriendlyException friendlyException = (SuyaaFriendlyException)ex;
                    ApiErrorResult errorResult = new ApiErrorResult()
                    {
                        Message = friendlyException.Message,
                        ErrorCode = friendlyException.ErrorCode,
                    };
                    await context.Response.Body.WriteAsync(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(errorResult)));
                }
                else
                {
                    ApiErrorResult errorResult = new ApiErrorResult()
                    {
                        Message = $"An error occurred while executing.",
                        ErrorCode = 0,
                    };
                    await context.Response.Body.WriteAsync(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(errorResult)));
                }
            }
        }
    }
}
