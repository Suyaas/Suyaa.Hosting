using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Suyaa.Hosting.Common.ActionFilters.Dependency;
using Suyaa.Hosting.Common.Attributes;
using Suyaa.Hosting.Common.Exceptions;
using Suyaa.Hosting.Infrastructure.Exceptions;
using Suyaa.Hosting.Infrastructure.Results;

namespace Suyaa.Hosting.Common.ActionFilters
{
    /// <summary>
    /// Api执行过滤器
    /// </summary>
    public class ApiActionFilter : IActionFilter
    {

        #region DI注入

        private readonly IActionFilterProvider _actionFilterProvider;

        /// <summary>
        /// Api执行过滤器
        /// </summary>
        public ApiActionFilter(
            IActionFilterProvider actionFilterProvider
            )
        {
            _actionFilterProvider = actionFilterProvider;
        }

        #endregion

        /// <summary>
        /// 执行后
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            // 支持标准输出特性
            var standardResult = context.ActionDescriptor.EndpointMetadata.Where(d => d is NotWrapperAttribute).FirstOrDefault();
            if (standardResult != null) return;
            try
            {
                var result = context.Result;
                // 处理异常
                if (context.Exception != null)
                {
                    sy.Logger.Error(context.Exception.ToString(), context.ActionDescriptor.DisplayName.Fixed());
                    context.ExceptionHandled = true;
                    context.Result = context.Exception.ToApiResult();
                    return;
                }
                // 处理空结果
                if (result is null)
                {
                    context.Result = new ApiResult();
                    return;
                }
                // 过滤标准类型
                if (result is ApiResult) return;
                var type = result.GetType();
                if (type.IsAssignableFrom(typeof(ApiResult))) return;
                // 空结果
                if (result is EmptyResult)
                {
                    context.Result = new ApiResult();
                    return;
                }
                // 对象结果
                if (result is ObjectResult)
                {
                    var obj = (ObjectResult)result;
                    if (obj.DeclaredType is null)
                    {
                        context.Result = new ApiResult();
                        return;
                    }
                    context.Result = new ApiResult<object>() { Data = obj.Value, DataType = obj.DeclaredType.Name };
                    return;
                }
                // 直接返回
                context.Result = new ApiResult<object>() { Data = result, DataType = type.Name };
            }
            catch (Exception ex)
            {
                throw new HostException(ex.Message);
            }
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
