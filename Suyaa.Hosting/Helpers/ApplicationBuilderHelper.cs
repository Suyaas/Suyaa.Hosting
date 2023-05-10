using Suyaa.Hosting.Middlewares;

namespace Suyaa.Hosting.Helpers
{
    /// <summary>
    /// 异常处理扩展
    /// </summary>
    public static class ApplicationBuilderHelper
    {
        /// <summary>
        /// 使用异常处理
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseFriendlyException(this IApplicationBuilder app) => app.UseMiddleware<ExceptionHandlerMiddleware>();
    }
}
