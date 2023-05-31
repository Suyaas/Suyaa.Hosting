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
        private readonly IJwtData _jwtData;
        private readonly IServiceProvider _provider;

        /// <summary>
        /// 应用认证数据
        /// </summary>
        public JwtAuthorizeFilter(
            IJwtData jwtData,
            IServiceProvider provider
            )
        {
            _jwtData = jwtData;
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
            var type = _jwtData.GetType();
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
            // 执行变更事件
            var pros = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var pro in pros)
            {
                pro.SetValue(_jwtData, pro.GetValue(info));
            }
            //_jwtData.Fill(info);
        }
    }
}
