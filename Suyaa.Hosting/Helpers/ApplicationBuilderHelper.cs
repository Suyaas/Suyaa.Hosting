using Suyaa.Hosting.Middlewares;
using Suyaa.Hosting.Options;

namespace Suyaa.Hosting.Helpers
{
    /// <summary>
    /// 应用扩展
    /// </summary>
    public static partial class ApplicationBuilderHelper
    {
        /// <summary>
        /// 使用异常处理
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseFriendlyException(this IApplicationBuilder app) => app.UseMiddleware<ExceptionHandlerMiddleware>();
    }
}
