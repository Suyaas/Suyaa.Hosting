using System.Reflection;

namespace Suyaa.Hosting.Infrastructure.Assemblies.Helpers
{
    /// <summary>
    /// MVC构建助手
    /// </summary>
    public static class MvcBuilderHelper
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
            foreach (var ass in assemblies)
            {
                builder.AddApplicationPart(ass);
            }
            return builder;
        }
    }
}
