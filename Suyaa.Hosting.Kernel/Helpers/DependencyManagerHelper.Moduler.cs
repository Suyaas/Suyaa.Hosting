using Suyaa.Configure;
using Suyaa.DependencyInjection;
using Suyaa.Hosting.Kernel.Dependency;
using System.Reflection;

namespace Suyaa.Hosting.Kernel.Helpers
{
    /* 容器扩展 - 模块相关 */
    public static partial class DependencyManagerHelper
    {
        // 核心服务接口类型
        private readonly static Type _serviceCoreType = typeof(IServiceCore);

        // 判断类型是否包含接口
        private static bool HasInterface<T>(Type? type)
        {
            var typeInterface = typeof(T);
            if (type is null) return false;
            if (!typeInterface.IsInterface) throw new Exception($"'{typeInterface.Name}'不是一个有效的接口");
            var ifs = type.GetInterfaces();
            foreach (var ifc in ifs)
            {
                if (ifc.Equals(typeInterface)) return true;
            }
            return false;
        }

        // 添加程序集
        private static void AddModulerAssemblyType(IDependencyManager dependency, Type? tp)
        {
            if (tp is null) return;
            if (!HasInterface<IModuleStartup>(tp)) return;
            sy.Logger.Info("AddModulerAssemblyType " + tp.FullName, "ModulerStartup");
            var obj = Activator.CreateInstance(tp);
            IModuleStartup? startup = (IModuleStartup?)obj;
            startup?.ConfigureDependency(dependency);
        }

        // 添加程序集
        private static void AddModulerAssembly(IDependencyManager dependency, Assembly? assembly)
        {
            sy.Logger.Info("AddModulerAssembly " + assembly?.Location, "ModulerStartup");
            // 遍历所有的IModulerStartup
            var tps = assembly?.GetTypes();
            if (tps != null)
            {
                foreach (var tp in tps)
                {
                    AddModulerAssemblyType(dependency, tp);
                }
            }
        }

        /// <summary>
        /// 添加控制器
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static void AddModuler(this IDependencyManager services, Assembly assembly)
        {
            AddModulerAssembly(services, assembly);
        }

        /// <summary>
        /// 添加控制器
        /// </summary>
        /// <param name="dependency"></param>
        /// <returns></returns>
        public static IDependencyManager AddModuler<T>(this IDependencyManager dependency) where T : IModuleStartup
        {
            AddModulerAssembly(dependency, typeof(T).Assembly);
            return dependency;
        }

        /// <summary>
        /// 添加控制器
        /// </summary>
        /// <param name="dependency"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static IDependencyManager AddModulers(this IDependencyManager dependency, List<Assembly> assemblies)
        {
            // 遍历所有的程序集
            foreach (var ass in assemblies)
            {
                AddModulerAssembly(dependency, ass);
            }
            return dependency;
        }

        /// <summary>
        /// 添加关联的依赖注入
        /// </summary>
        /// <param name="dependency"></param>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static IDependencyManager AddModulerIoc(this IDependencyManager dependency, Assembly assembly)
        {
            sy.Logger.Info("AddModulerIoc " + (assembly.Location ?? ""), "ModulerStartup");
            // 注册程序集
            dependency.RegisterAssembly(assembly);
            return dependency;
        }

        /// <summary>
        /// 添加关联的依赖注入
        /// </summary>
        /// <param name="dependency"></param>
        /// <returns></returns>
        public static IDependencyManager AddModulerIoc<T>(this IDependencyManager dependency) where T : IModuleStartup
        {
            dependency.AddModulerIoc(typeof(T).Assembly);
            return dependency;
        }
    }
}
