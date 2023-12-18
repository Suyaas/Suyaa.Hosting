using Microsoft.AspNetCore.Http;
using Suyaa.Hosting.Common.Configures.Dependency;
using Suyaa.Hosting.Redirects.Configures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Hosting.Redirects.Middlewares
{
    /// <summary>
    /// 重定向中间件
    /// </summary>
    public sealed class RedirectMiddleware
    {
        private readonly RedirectConfig _redirectConfig;
        private readonly RequestDelegate _next;

        /// <summary>
        /// 重定向中间件
        /// </summary>
        public RedirectMiddleware(
            RequestDelegate next,
            IOptionConfig<RedirectConfig> redirectConfig
            )
        {
            _redirectConfig = redirectConfig.CurrentValue;
            _next = next;
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            // 未启动重定向，则跳过
            if (!_redirectConfig.Enable)
            {
                await _next(context);
                return;
            }
            // 查找配置
            var redirect = _redirectConfig.Redirects.Where(d => d.Source == context.Request.Path).FirstOrDefault();
            if (redirect is null)
            {
                await _next(context);
                return;
            }
            // 执行跳转
            context.Response.Redirect(redirect.Dest);
        }
    }
}
