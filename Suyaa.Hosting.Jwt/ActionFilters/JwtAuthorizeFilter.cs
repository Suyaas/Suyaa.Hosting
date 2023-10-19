using Microsoft.AspNetCore.Mvc.Filters;
using Suyaa.DependencyInjection;
using Suyaa.Hosting.Jwt.Dependency;
using Suyaa.Hosting.Kernel;
using Suyaa.Hosting.Kernel.Dependency;

namespace Suyaa.Hosting.Jwt.ActionFilters
{
    /// <summary>
    /// 应用认证数据
    /// </summary>
    public class JwtAuthorizeFilter : IActionFilter, IFilterMetadata
    {

        #region DI注入
        private readonly IJwtManager _jwtManager;
        private readonly ISession _session;
        private readonly IDependencyManager _dependency;

        //private readonly II18n _i18n;

        /// <summary>
        /// 应用认证数据
        /// </summary>
        public JwtAuthorizeFilter(
            IJwtManager jwtDataManager,
            ISession session,
            //II18n i18n,
            IDependencyManager dependency
            )
        {
            _jwtManager = jwtDataManager;
            _session = session;
            _dependency = dependency;
            //_i18n = i18n;
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
            // 优先检测头部信息
            string token = string.Empty;
            if (request.Headers.ContainsKey(sy.Jwt.TokenName))
            {
                token = request.Headers[sy.Jwt.TokenName].ToString();
            }
            // 头部信息中没有，则从Cookie中获取
            if (request.Cookies.ContainsKey(sy.Jwt.TokenName))
            {
                token = request.Cookies[sy.Jwt.TokenName] ?? string.Empty;
            }
            if(token.IsNullOrWhiteSpace()) throw new HostFriendlyException($"Jwt invalid.");
            // 检测Jwt信息
            IJwtData jwtData = _jwtManager.GetCurrentData();
            try
            {
                jwtData = (IJwtData)sy.Jwt.GetData(token, jwtData.GetType());
                _jwtManager.SetCurrentData(jwtData);
                _session.Uid = jwtData.Uid;
                _session.TenantId = jwtData.TenantId;
                _session.InvalidTime = DateTime.Now.AddHours(1);
            }
            catch (HostException ex)
            {
                throw new HostFriendlyException(ex.Message);
            }
            if (jwtData.Uid.IsNullOrWhiteSpace()) throw new HostFriendlyException($"Jwt invalid.");
            //_jwtDataManager.Data = info;
        }
    }
}
