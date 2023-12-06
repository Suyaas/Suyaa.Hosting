using Microsoft.AspNetCore.Mvc.Filters;
using Suyaa.Hosting.Common.ActionFilters.Dependency;
using Suyaa.Hosting.Common.DependencyManager.Dependency;
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
        private readonly List<Type> _types;

        /// <summary>
        /// Api执行过滤器
        /// </summary>
        public ApiAsyncActionFilter(
            IDependencyManager dependencyManager
            )
        {
            _dependencyManager = dependencyManager;
            _types = _dependencyManager.GetResolveTypes<IActionFilterProvider>();
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
            // 所有切片初始化
            List<IActionFilterProvider> actionFilterProviders = new List<IActionFilterProvider>();
            foreach (var type in _types)
            {
                actionFilterProviders.Add((IActionFilterProvider)_dependencyManager.Resolve(type));
            }
            // 执行前置逻辑
            foreach (var provider in actionFilterProviders)
            {
                provider.OnActionExecuting(context);
            }
            var contextExecuted = await next();
            // 执行后置逻辑
            foreach (var provider in actionFilterProviders)
            {
                provider.OnActionExecuted(contextExecuted);
            }
        }
    }
}
