using Suyaa.Hosting.Kernel;
using Microsoft.AspNetCore.Http;
using Suyaa.Hosting.Kernel.Results;

namespace Suyaa.Hosting.Middlewares
{
    /// <summary>
    /// 异常中间件
    /// </summary>
    public class ExceptionHandlerMiddleware
    {
        // 变量
        private RequestDelegate _next;

        /// <summary>
        /// 异常中间件
        /// </summary>
        /// <param name="next"></param>
        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
            sy.Logger.Info("初始化 ExceptionHandlerMiddleware ...", "Middleware");
        }

        // 触发友好异常
        private async Task<bool> RaiseFriendlyException(HttpContext context, Exception ex)
        {
            switch (ex)
            {
                case HostFriendlyException friendlyException:
                    ApiErrorResult errorResult = new ApiErrorResult()
                    {
                        Message = friendlyException.Message,
                        ErrorCode = friendlyException.ErrorCode,
                    };
                    await errorResult.ExecuteResultAsync(context);
                    return true;
                default:
                    if (ex.InnerException is null) return false;
                    return await RaiseFriendlyException(context, ex.InnerException);
            }
        }

        /// <summary>
        /// 执行
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
                // 错误记录
                sy.Logger.Error(ex.ToString(), context.Request.Path);
                // 优先触发友好异常，如未找到友好异常，则输出标准异常
                if (!await RaiseFriendlyException(context, ex))
                {
                    ApiErrorResult errorResult = new ApiErrorResult()
                    {
                        Message = $"An error occurred while executing.",
                        ErrorCode = 0,
                    };
                    await errorResult.ExecuteResultAsync(context);
                }
            }
        }
    }
}
