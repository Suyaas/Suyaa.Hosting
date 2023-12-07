using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Suyaa.Hosting.App.FeatureProviders;
using Suyaa.Hosting.App.ModelConventions;
using Suyaa.Hosting.App.Options;
using Suyaa.Hosting.Common.ActionFilters;
using Suyaa.Hosting.Common.Resources;
using Suyaa.Hosting.Core.WebApplications;
using Suyaa.Hosting.Infrastructure.Assemblies.Helpers;

namespace Suyaa.Hosting.App.WebApplications
{
    /// <summary>
    /// Web应用启动器
    /// </summary>
    public class AppStartup : CoreStartup
    {
        /// <summary>
        /// 服务配置
        /// </summary>
        /// <param name="services"></param>
        protected override void OnConfigureServices(IServiceCollection services)
        {
            base.OnConfigureServices(services);

            #region 添加控制器配置
            // 添加服务配置
            ServiceOption option = new ServiceOption();
            option.RouteUrl = HostConfig.DomainService.RouteRoot;
            option.AddAssemblies(this.Assemblies);
            // 获取所有的

            // 根据配置添加所有的控制器
            if (HostConfig.IsControllerSupported)
            {
                services.AddControllers(options =>
                {
                    // 添加过滤器
                    sy.Logger.Debug($"Add {typeof(ApiActionFilter).FullName}", LogEvents.Filter);
                    options.Filters.Add<ApiActionFilter>();
                    sy.Logger.Debug($"Add {typeof(ApiAsyncActionFilter).FullName}", LogEvents.Filter);
                    options.Filters.Add<ApiAsyncActionFilter>();
                    //foreach (var filter in _filters)
                    //{
                    //    sy.Logger.Debug($"Add {filter.FullName}", LogEvents.Filter);
                    //    options.Filters.Add(filter);
                    //}
                    // 添加约定器
                    options.Conventions.Add(new ServiceApplicationModelConvention(option));
                    options.Conventions.Add(new ServiceActionModelConvention());
                }, this.Assemblies).ConfigureApplicationPartManager(pm =>
                {
                    // 添加自定义控制器
                    pm.FeatureProviders.Add(new ServiceControllerFeatureProvider(option));
                });
            }
            #endregion
        }
    }
}
