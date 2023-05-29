using Microsoft.AspNetCore.Mvc.Filters;
using Suyaa.Hosting;
using Suyaa.Hosting.Helpers;

namespace Suyaa.Hosting.Dependency
{
    /// <summary>
    /// Jwt登录校验
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public abstract class JwtAuthorizeBaseAttribute : ActionFilterAttribute
    {
        private readonly Type _type;

        /// <summary>
        /// Jwt登录校验
        /// </summary>
        /// <param name="type"></param>
        public JwtAuthorizeBaseAttribute(Type type)
        {
            if (!type.HasInterface<IJwtData>()) throw new HostFriendlyException($"Type '{type.FullName}' not implemented interface 'IJwtData'.");
            _type = type;
        }

        /// <summary>
        /// 接口执行
        /// </summary>
        /// <param name="context"></param>
        /// <exception cref="HostFriendlyException"></exception>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // 检测头部信息
            var request = context.HttpContext.Request;
            if (!request.Headers.ContainsKey(sy.Jwt.TokenName)) throw new HostFriendlyException($"Jwt invalid.");
            string token = request.Headers[sy.Jwt.TokenName].ToString();
            // 检测Jwt信息
            IJwtData info;
            try
            {
                info = (IJwtData)sy.Jwt.GetData(token, _type);
            }
            catch (HostException ex)
            {
                throw new HostFriendlyException(ex.Message);
            }
            if (info.UserId <= 0) throw new HostFriendlyException($"Jwt invalid.");
            // 填充信息
            context.RouteData.Values[sy.Jwt.RouteName] = info;
            base.OnActionExecuting(context);
        }
    }
}
