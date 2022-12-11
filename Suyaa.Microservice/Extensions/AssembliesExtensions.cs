using System.Reflection;

namespace Suyaa.Microservice.Extensions
{
    /// <summary>
    /// 程序集扩展
    /// </summary>
    public static class AssembliesExtensions
    {
        /// <summary>
        /// 添加程序集列表
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static IMvcBuilder AddAssemblyList(this IMvcBuilder builder, List<Assembly> assemblies)
        {
            // 添加程序集
            foreach (var ass in assemblies) builder.AddApplicationPart(ass);
            return builder;
        }

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
    }
}
