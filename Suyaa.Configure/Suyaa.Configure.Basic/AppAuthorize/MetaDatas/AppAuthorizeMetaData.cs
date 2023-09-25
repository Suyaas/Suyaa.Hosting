using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Suyaa.Configure.Basic.AppAuthorize.Dependency;
using Suyaa.DependencyInjection;
using Suyaa.Hosting;
using Suyaa.Hosting.Kernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Configure.Basic.AppAuthorize.MetaDatas
{
    /// <summary>
    /// 应用认证数据
    /// </summary>
    public class AppAuthorizeMetaData : IFilterMetadata, IDependencyTransient
    {

        /// <summary>
        /// 应用Id
        /// </summary>
        private const string APP_ID = "App-Id";
        /// <summary>
        /// 应用密钥
        /// </summary>
        private const string APP_KEY = "App-Key";

        #region DI注入
        private readonly IAppInfo _appInfo;
        private readonly IServiceProvider _provider;
        private readonly IHttpContextAccessor _accessor;

        /// <summary>
        /// 应用认证数据
        /// </summary>
        /// <param name="provider"></param>
        public AppAuthorizeMetaData(
            IAppInfo appInfo,
            IServiceProvider provider,
            IHttpContextAccessor accessor
            )
        {
            _appInfo = appInfo;
            _provider = provider;
            _accessor = accessor;
        }
        #endregion

        /// <summary>
        /// 设置信息
        /// </summary>
        /// <param name="appInfo"></param>
        public void SetInfo()
        {
            // 检测头部信息
            var request = _accessor.HttpContext?.Request;
            if (request is null) throw new HostFriendlyException($"App authorize invalid.");
            if (!request.Headers.ContainsKey(APP_ID)) throw new HostFriendlyException($"App authorize invalid.");
            if (!request.Headers.ContainsKey(APP_KEY)) throw new HostFriendlyException($"App authorize invalid.");
            string appId = request.Headers[APP_ID].ToString();
            string appKey = request.Headers[APP_KEY].ToString();
            _appInfo.AppId = appId;
            _appInfo.AppKey = appKey;
        }
    }
}
