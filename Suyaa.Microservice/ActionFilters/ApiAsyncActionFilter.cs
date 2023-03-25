using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Suyaa.Microservice.Exceptions;
using Suyaa.Microservice.Results;

namespace Suyaa.Microservice.ActionFilters
{
    /// <summary>
    /// Api执行过滤器
    /// </summary>
    public class ApiAsyncActionFilter : IAsyncActionFilter
    {
        /// <summary>
        /// 处理执行
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        /// <exception cref="FriendlyException"></exception>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            await next();
            try
            {
                var result = context.Result;
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
                throw new FriendlyException(ex.Message);
            }
        }
    }
}
