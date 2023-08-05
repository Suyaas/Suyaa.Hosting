using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Suyaa.Hosting.Dependency;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Hosting.ActionFilters
{
    /// <summary>
    /// 应用认证数据
    /// </summary>
    public class JwtAuthorizeFilter : IActionFilter, IFilterMetadata
    {

        #region DI注入
        private readonly IJwtDataManager _jwtDataManager;
        private readonly IJwtDataType _jwtDataType;
        private readonly II18n _i18n;
        private readonly IServiceProvider _provider;

        /// <summary>
        /// 应用认证数据
        /// </summary>
        public JwtAuthorizeFilter(
            IJwtDataManager jwtDataManager,
            IJwtDataType jwtDataType,
            II18n i18n,
            IServiceProvider provider
            )
        {
            _jwtDataManager = jwtDataManager;
            _jwtDataType = jwtDataType;
            _i18n = i18n;
            _provider = provider;
        }
        #endregion

        /// <summary>
        /// 执行结束
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// 执行中
        /// </summary>
        /// <param name="context"></param>
        /// <exception cref="HostFriendlyException"></exception>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // 检测头部信息
            var request = context.HttpContext.Request;
            if (request is null) throw new HostFriendlyException($"Jwt invalid.");
            // 检测头部信息
            if (!request.Headers.ContainsKey(sy.Jwt.TokenName)) throw new HostFriendlyException($"Jwt invalid.");
            string token = request.Headers[sy.Jwt.TokenName].ToString();
            // 数据类型
            var type = _jwtDataType.Type;
            // 检测Jwt信息
            IJwtData info;
            try
            {
                info = (IJwtData)sy.Jwt.GetData(token, type);
            }
            catch (HostException ex)
            {
                throw new HostFriendlyException(ex.Message);
            }
            if (info.UserId <= 0) throw new HostFriendlyException($"Jwt invalid.");
            _jwtDataManager.Data = info;
        }
    }
}
