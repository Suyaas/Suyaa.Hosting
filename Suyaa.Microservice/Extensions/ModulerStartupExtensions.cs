using System.Reflection;

namespace Suyaa.Microservice.Extensions
{
    /// <summary>
    /// 模块启动器扩展
    /// </summary>
    public static class ModulerStartupExtensions
    {
        // 添加程序集
        private static void AddModulerAssemblyType(IServiceCollection services, Type? tp)
        {
            egg.Logger.Info(tp?.FullName ?? "", "AddModulers");
            var ifs = tp?.GetInterfaces().Where(x => x == typeof(IModulerStartup));
            if (ifs?.Any() ?? false)
            {
                if (tp != null)
                {
                    IModulerStartup? startup = (IModulerStartup?)Activator.CreateInstance(tp);
                    startup?.ConfigureServices(services);
                }
            }
        }

        // 添加程序集
        private static void AddModulerAssembly(IServiceCollection services, Assembly? assembly)
        {
            egg.Logger.Info(assembly?.Location ?? "", "AddModulers");
            // 遍历所有的IModulerStartup
            var tps = assembly?.GetTypes();
            if (tps != null)
            {
                foreach (var tp in tps)
                {
                    AddModulerAssemblyType(services, tp);
                }
            }
        }

        /// <summary>
        /// 添加控制器
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static void AddModuler(this IServiceCollection services, Assembly assembly)
        {
            AddModulerAssembly(services, assembly);
        }

        /// <summary>
        /// 添加控制器
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static void AddModuler<T>(this IServiceCollection services) where T : IModulerStartup
        {
            AddModulerAssembly(services, typeof(T).Assembly);
        }

        /// <summary>
        /// 添加控制器
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static void AddModulers(this IServiceCollection services, List<Assembly> assemblies)
        {
            // 遍历所有的程序集
            foreach (var ass in assemblies)
            {
                AddModulerAssembly(services, ass);
            }
        }

        /// <summary>
        /// 添加关联的依赖注入
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static void AddModulerIoc(this IServiceCollection services, Assembly assembly)
        {
            egg.Logger.Info(assembly?.Location ?? "", "AddModulerIoc");
            // 遍历所有的IModulerStartup
            //var tps = ass?.GetTypes().Where(d => d.BaseType == typeof(IModulerStartup));
            var tps = assembly?.GetTypes();
            if (tps != null)
            {
                foreach (var tp in tps)
                {
                    var ifs = tp?.GetInterfaces();
                    if (ifs != null)
                    {
                        foreach (var i in ifs)
                        {
                            egg.Logger.Info((i?.FullName ?? "") + ": " + (tp?.FullName ?? ""), "AddModulerIoc");
                            if (i != null && tp != null) services.AddSingleton(i, tp);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 添加关联的依赖注入
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static void AddModulerIoc<T>(this IServiceCollection services) where T : IModulerStartup
        {
            services.AddModulerIoc(typeof(T).Assembly);
        }
    }
}
