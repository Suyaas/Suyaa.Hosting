using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Suyaa.Hosting.Dependency;
using Suyaa.Hosting.Kernel;
using Suyaa.Hosting.Kernel.Attributes;
using Suyaa.Hosting.Kernel.Helpers;
using Suyaa.Hosting.Kernel.Results;

namespace Suyaa.Hosting.Kernel.ActionFilters
{
    /// <summary>
    /// Api执行过滤器
    /// </summary>
    public class ApiActionFilter : IActionFilter
    {
        #region DI注入

        private readonly IMultilingualManager _multilingualManager;

        /// <summary>
        /// Api执行过滤器
        /// </summary>
        public ApiActionFilter(
            IMultilingualManager multilingualManager
            )
        {
            _multilingualManager = multilingualManager;
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
                throw new HostFriendlyException(ex.Message);
            }
        }

        /// <summary>
        /// 执行中
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            //sy.Logger.Debug(context.HttpContext.Request.Path);
        }
    }
}
