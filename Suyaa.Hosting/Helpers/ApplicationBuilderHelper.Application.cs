using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Suyaa.Hosting.Dependency;
using Suyaa.Hosting.Middlewares;
using Suyaa.Hosting.Options;
using System.Reflection;

namespace Suyaa.Hosting.Helpers
{
    /* 容器扩展 - 应用相关 */
    public static partial class ApplicationBuilderHelper
    {
        /// <summary>
        /// 使用应用服务
        /// </summary>
        /// <param name="app"></param>
        /// <param name="routeUrl"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseAppliction(this IApplicationBuilder app, string routeUrl, Type type)
        {
            // 动态将Controller注册到路由中
            var routeBuilder = new RouteBuilder(app);
            routeBuilder.MapRoute(name: type.FullName, template: routeUrl, defaults: new { controller = type.Name, action = "Index" });
            var router = routeBuilder.Build();
            app.UseRouter(router);
            return app;
        }

        /// <summary>
        /// 使用应用服务
        /// </summary>
        /// <param name="app"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseApplictions(this IApplicationBuilder app, Action<ApplicationOption> action)
        {
            ApplicationOption option = new ApplicationOption();
            action(option);
            foreach (var type in option.Types) app.UseAppliction(option.RouteUrl, type);
            return app;
        }
    }
}
