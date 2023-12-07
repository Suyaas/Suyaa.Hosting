using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Suyaa.Hosting.Common.ActionFilters.Dependency
{
    /// <summary>
    /// Api执行结束供应商
    /// </summary>
    public interface IApiExecutedProvider
    {
        /// <summary>
        /// 执行成功
        /// </summary>
        /// <param name="result"></param>
        IActionResult? OnSuccess(IActionResult? result);
        /// <summary>
        /// 执行异常
        /// </summary>
        /// <param name="ex"></param>
        IActionResult? OnError(Exception ex);
    }
}
