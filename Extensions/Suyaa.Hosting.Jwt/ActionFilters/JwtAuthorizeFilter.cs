using Microsoft.AspNetCore.Mvc.Filters;
using Suyaa.Hosting.Common.Configures;
using Suyaa.Hosting.Common.Configures.Dependency;
using Suyaa.Hosting.Common.DependencyInjection.Dependency;
using Suyaa.Hosting.Common.Exceptions;
using Suyaa.Hosting.Common.Sessions.Dependency;
using Suyaa.Hosting.Infrastructure.Exceptions;
using Suyaa.Hosting.Jwt.Configures;
using Suyaa.Hosting.Jwt.Dependency;

namespace Suyaa.Hosting.Jwt.ActionFilters
{
    /// <summary>
    /// 应用认证数据
    /// </summary>
    public class JwtAuthorizeFilter<TData> : IActionFilter, IFilterMetadata
        where TData : class, IJwtData, new()
    {

        #region DI注入
        private readonly IJwtManager<TData> _jwtManager;
        private readonly JwtConfig _jwtOption;
        private readonly ISessionManager _sessionManager;
        private readonly ISession _session;
        private readonly IDependencyManager _dependency;

        //private readonly II18n _i18n;

        /// <summary>
        /// 应用认证数据
        /// </summary>
        public JwtAuthorizeFilter(
            IJwtManager<TData> jwtDataManager,
            IOptionConfig<JwtConfig> jwtOption,
            ISessionManager sessionManager,
            //II18n i18n,
            IDependencyManager dependency
            )
        {
            _jwtManager = jwtDataManager;
            _jwtOption = jwtOption.CurrentValue;
            _sessionManager = sessionManager;
            _session = _sessionManager.GetSession();
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
        /// <exception cref="UserFriendlyException"></exception>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // 检测头部信息
            var request = context.HttpContext.Request;
            if (request is null) throw new UserFriendlyException($"Jwt invalid.");
            // 优先检测头部信息
            string token = string.Empty;
            if (_jwtOption.IsHeaderSupported && request.Headers.ContainsKey(_jwtOption.TokenName))
            {
                token = request.Headers[_jwtOption.TokenName].ToString();
            }
            // 头部信息中没有，则从Cookie中获取
            if (_jwtOption.IsCookieSupported && request.Cookies.ContainsKey(_jwtOption.TokenName))
            {
                token = request.Cookies[_jwtOption.TokenName] ?? string.Empty;
            }
            if (token.IsNullOrWhiteSpace()) throw new UserFriendlyException($"Jwt invalid.");
            // 检测Jwt信息
            TData jwtData = _jwtManager.GetCurrentData();
            try
            {
                jwtData = _jwtManager.Provider.Builder.GetData(token);
                _jwtManager.SetCurrentData(jwtData);
                _session.Set("Uid", jwtData.Uid);
                _session.Set("TenantId", jwtData.TenantId);
                _session.Set("InvalidTime", DateTime.Now.AddHours(1));
            }
            catch (HostException ex)
            {
                throw new UserFriendlyException(ex.Message);
            }
            if (jwtData.Uid.IsNullOrWhiteSpace()) throw new UserFriendlyException($"Jwt invalid.");
            //_jwtDataManager.Data = info;
        }
    }
}
