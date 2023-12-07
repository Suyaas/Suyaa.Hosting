using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Suyaa.Hosting.Common.ActionFilters.Dependency;
using Suyaa.Hosting.Common.Exceptions;
using Suyaa.Hosting.Infrastructure.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Hosting.Common.ActionFilters
{
    /// <summary>
    /// Api执行结束处理供应商
    /// </summary>
    public class ApiExecutedProvider : IApiExecutedProvider
    {
        /// <summary>
        /// 执行异常
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public IActionResult? OnError(Exception ex)
        {
            return ex.ToApiResult();
        }

        /// <summary>
        /// 执行成功
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public IActionResult? OnSuccess(IActionResult? result)
        {
            // 处理空结果
            if (result is null) return new ApiResult();
            // 过滤标准类型
            if (result is ApiResult) return result;
            var type = result.GetType();
            if (type.IsBased<ApiResult>()) return result;
            // 空结果
            if (result is EmptyResult) return new ApiResult();
            // 对象结果
            if (result is ObjectResult)
            {
                var obj = (ObjectResult)result;
                if (obj.DeclaredType is null) return new ApiResult();
                return new ApiResult<object>() { Data = obj.Value };
            }
            // 直接返回
            return new ApiResult<object>() { Data = result };
        }
    }
}
