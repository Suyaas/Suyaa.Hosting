using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Suyaa.Hosting.Redirects.Middlewares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Hosting.Redirects.Helpers
{
    /// <summary>
    /// 应用助手
    /// </summary>
    public static class WebApplicationHelper
    {
        /// <summary>
        /// 使用重定向中间件
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseRedirects(this IApplicationBuilder app) => app.UseMiddleware<RedirectMiddleware>();
    }
}
