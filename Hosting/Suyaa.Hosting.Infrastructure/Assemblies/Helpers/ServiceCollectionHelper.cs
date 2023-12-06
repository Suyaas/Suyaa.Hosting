using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Suyaa.Hosting.Infrastructure.Assemblies.Helpers;
using System.Reflection;

namespace Suyaa.Hosting.Infrastructure.Assemblies.Helpers
{
    /// <summary>
    /// 容器扩展
    /// </summary>
    public static class ServiceCollectionHelper
    {

        /// <summary>
        /// 添加控制器
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static IMvcBuilder AddControllers(this IServiceCollection services, List<Assembly> assemblies)
        {
            return services.AddControllers().AddAssemblyList(assemblies);
        }

        /// <summary>
        /// 添加控制器
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configure"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static IMvcBuilder AddControllers(this IServiceCollection services, Action<MvcOptions>? configure, List<Assembly> assemblies)
        {
            return services.AddControllers(configure).AddAssemblyList(assemblies);
        }

        /// <summary>
        /// 添加控制器
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static IMvcBuilder AddRazorPages(this IServiceCollection services, List<Assembly> assemblies)
        {
            return services.AddRazorPages().AddAssemblyList(assemblies);
        }
    }
}
