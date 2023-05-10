using Suyaa;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Suyaa.Hosting.Helpers;
using Suyaa.Hosting.Results;
using Suyaa.Helpers;

namespace Suyaa.Hosting.ActionFilters
{
    /// <summary>
    /// Api执行过滤器
    /// </summary>
    public class ApiActionFilter : IActionFilter
    {

        /// <summary>
        /// 执行后
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            try
            {
                var result = context.Result;
                // 处理异常
                if (context.Exception != null)
                {
                    sy.Logger.Error(context.Exception.ToString(), context.ActionDescriptor.DisplayName.ToNotNull());
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
                    context.Result = new ApiResult<object>() { Data = obj.Value, DataType = obj.DeclaredType.FullName };
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
