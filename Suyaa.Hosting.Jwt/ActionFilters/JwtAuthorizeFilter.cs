using Microsoft.AspNetCore.Mvc.Filters;
using Suyaa.DependencyInjection;
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
            // 检测头部信息
            if (!request.Headers.ContainsKey(sy.Jwt.TokenName)) throw new HostFriendlyException($"Jwt invalid.");
            string token = request.Headers[sy.Jwt.TokenName].ToString();
            // 数据类型
            //var type = _jwtDataType.Type;
            // 检测Jwt信息
            IJwtData info;
            try
            {
                var types = _dependency.GetResolveTypes(typeof(IJwtData));
                if (!types.Any()) throw new HostFriendlyException("Jwt data type missing.");
                info = (IJwtData)sy.Jwt.GetData(token, types.First());
                _jwtManager.SetCurrentData(info);
                _session.Uid = info.Uid;
                _session.TenantId = info.TenantId;
                _session.InvalidTime = DateTime.Now.AddHours(1);
            }
            catch (HostException ex)
            {
                throw new HostFriendlyException(ex.Message);
            }
            if (info.Uid.IsNullOrWhiteSpace()) throw new HostFriendlyException($"Jwt invalid.");
            //_jwtDataManager.Data = info;
        }
    }
}
