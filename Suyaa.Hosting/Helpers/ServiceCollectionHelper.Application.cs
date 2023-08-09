using Suyaa.Hosting.Applications;
using Suyaa.Hosting.Dependency;
using Suyaa.Hosting.Middlewares;
using Suyaa.Hosting.Options;
using System.Reflection;

namespace Suyaa.Hosting.Helpers
{
    /* 容器扩展 - 应用相关 */
    public static partial class ServiceCollectionHelper
    {
        /// <summary>
        /// 使用应用服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IServiceCollection AddApplictions(this IServiceCollection services, Action<ApplicationOption> action)
        {
            ApplicationOption option = new ApplicationOption();
            action(option);
            //foreach (var type in option.Types) app.UseAppliction(option.RouteUrl, type);
            services.AddSingleton(new ApplicationEndPointDataSourceFactory(option));
            foreach (var type in option.Types) services.AddTransient(type);
            return services;
        }
    }
}
