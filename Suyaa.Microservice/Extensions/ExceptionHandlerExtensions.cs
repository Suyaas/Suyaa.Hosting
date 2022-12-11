using Suyaa.Microservice.Middlewares;

namespace Suyaa.Microservice.Extensions
{
    /// <summary>
    /// 异常处理扩展
    /// </summary>
    public static class ExceptionHandlerExtensions
    {
        /// <summary>
        /// 使用异常处理
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseFriendlyException(this IApplicationBuilder app) => app.UseMiddleware<ExceptionHandlerMiddleware>();
    }
}
