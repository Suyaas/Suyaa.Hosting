using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Suyaa.Hosting.Common.ActionFilters.Dependency;
using Suyaa.Hosting.Common.Attributes;
using Suyaa.Hosting.Common.DependencyInjection.Dependency;
using Suyaa.Hosting.Common.Exceptions;
using Suyaa.Hosting.Infrastructure.Exceptions;
using Suyaa.Hosting.Infrastructure.Results;

namespace Suyaa.Hosting.Redirects.ActionFilters
{
    /// <summary>
    /// Api执行过滤器
    /// </summary>
    public class RedirectActionFilter : IActionFilter
    {
        #region DI注入

        private readonly IApiExecutedProvider _apiExecutedProvider;

        /// <summary>
        /// Api执行过滤器
        /// </summary>
        public RedirectActionFilter(
            IApiExecutedProvider apiExecutedProvider
            )
        {
            _apiExecutedProvider = apiExecutedProvider;
        }

        #endregion

        /// <summary>
        /// 执行后
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        /// <summary>
        /// 执行中
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            sy.Logger.Debug(context.HttpContext.Request.Path);
        }
    }
}
