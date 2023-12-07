using Microsoft.AspNetCore.Mvc.Filters;

namespace Suyaa.Hosting.Common.ActionFilters.Dependency
{
    /// <summary>
    /// 切片供应商
    /// </summary>
    public interface IActionFilterProvider
    {
        /// <summary>
        /// 执行结束
        /// </summary>
        /// <param name="context"></param>
        void OnActionExecuted(ActionExecutedContext context);
        /// <summary>
        /// 执行中
        /// </summary>
        /// <param name="context"></param>
        void OnActionExecuting(ActionExecutingContext context);
    }
}
