using Microsoft.AspNetCore.Mvc.Filters;
using Suyaa.DependencyInjection;
using Suyaa.Hosting.Dependency;
using Suyaa.Hosting.Kernel.Dependency;

namespace Suyaa.Hosting.Kernel.ActionFilters
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
        /// <exception cref="HostFriendlyException"></exception>
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
            //try
            //{
            //    var result = context.Result;
            //    if (result is null)
            //    {
            //        context.Result = new ApiResult();
            //        return;
            //    }
            //    // 过滤标准类型
            //    if (result is ApiResult) return;
            //    var type = result.GetType();
            //    if (type.IsAssignableFrom(typeof(ApiResult))) return;
            //    // 空结果
            //    if (result is EmptyResult)
            //    {
            //        context.Result = new ApiResult();
            //        return;
            //    }
            //    // 对象结果
            //    if (result is ObjectResult)
            //    {
            //        var obj = (ObjectResult)result;
            //        if (obj.DeclaredType is null)
            //        {
            //            context.Result = new ApiResult();
            //            return;
            //        }
            //        context.Result = new ApiResult<object>() { Data = obj.Value, DataType = obj.DeclaredType.Name };
            //        return;
            //    }
            //    // 直接返回
            //    context.Result = new ApiResult<object>() { Data = result, DataType = type.Name };
            //}
            //catch (Exception ex)
            //{
            //    // 错误记录
            //    egg.Logger.Error(ex.ToString(), context.ActionDescriptor.DisplayName ?? string.Empty);
            //    context.Result = ex.ToApiResult();
            //}
        }
    }
}
