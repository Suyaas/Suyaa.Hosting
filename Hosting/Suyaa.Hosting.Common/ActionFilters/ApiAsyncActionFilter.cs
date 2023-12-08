using Microsoft.AspNetCore.Mvc.Filters;
using Suyaa.Hosting.Common.ActionFilters.Dependency;
using Suyaa.Hosting.Common.DependencyInjection.Dependency;
using Suyaa.Hosting.Common.DependencyInjection.Helpers;
using Suyaa.Hosting.Common.Exceptions;

namespace Suyaa.Hosting.Common.ActionFilters
{
    /// <summary>
    /// Api执行过滤器
    /// </summary>
    public class ApiAsyncActionFilter : IAsyncActionFilter
    {
        #region DI注入

        private readonly IDependencyManager _dependencyManager;
        private readonly IEnumerable<IActionFilterProvider> _providers;

        /// <summary>
        /// Api执行过滤器
        /// </summary>
        public ApiAsyncActionFilter(
            IDependencyManager dependencyManager
            )
        {
            _dependencyManager = dependencyManager;
            _providers = _dependencyManager.Resolves<IActionFilterProvider>();
        }

        #endregion

        /// <summary>
        /// 处理执行
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // 执行前置逻辑
            foreach (var provider in _providers)
            {
                provider.OnActionExecuting(context);
            }
            var contextExecuted = await next();
            // 执行后置逻辑
            foreach (var provider in _providers)
            {
                provider.OnActionExecuted(contextExecuted);
            }
        }
    }
}
