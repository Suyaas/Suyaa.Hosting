using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Suyaa.Configure.Basic.AppAuthorize.MetaDatas;
using Suyaa.Hosting.Dependency;
using Suyaa.Hosting.Helpers;
using Suyaa.Hosting.Kernel;

namespace Suyaa.Configure.Basic.AppAuthorize.Attributes
{
    /// <summary>
    /// 应用授权校验
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class AppAuthorizeAttribute : ActionFilterAttribute
        , IFilterFactory
    {
        /// <summary>
        /// 应用Id
        /// </summary>
        private const string APP_ID = "App-Id";
        /// <summary>
        /// 应用密钥
        /// </summary>
        private const string APP_KEY = "App-Key";

        /// <summary>
        /// Jwt登录校验
        /// </summary>
        /// <param name="type"></param>
        public AppAuthorizeAttribute()
        {
        }

        public bool IsReusable => false;

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            var metaData = serviceProvider.GetService<AppAuthorizeMetaData>();
            if (metaData is null) throw new HostFriendlyException($"AppAuthorizeFilterMetaData create fail.");
            metaData.SetInfo();
            return metaData;
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
            if (!request.Headers.ContainsKey(APP_ID)) throw new HostFriendlyException($"App authorize invalid.");
            if (!request.Headers.ContainsKey(APP_KEY)) throw new HostFriendlyException($"App authorize invalid.");
            string appId = request.Headers[APP_ID].ToString();
            string appKey = request.Headers[APP_KEY].ToString();
            //// 设置应用信息
            //if (_metaData is null) throw new HostFriendlyException($"AppAuthorizeFilterMetaData create fail.");
            //_metaData.SetInfo(new AppInfo()
            //{
            //    AppId = appId,
            //    AppKey = appKey,
            //});
            //if (info.UserId <= 0) throw new HostFriendlyException($"Jwt invalid.");
            //// 填充信息
            //context.RouteData.Values[sy.Jwt.RouteName] = info;
            base.OnActionExecuting(context);
        }
    }
}
