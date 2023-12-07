using Suyaa.Hosting.Common.DependencyInjection;
using Suyaa.Hosting.Common.DependencyInjection.Dependency;
using Suyaa.Hosting.Common.DependencyInjection.Helpers;
using Suyaa.Hosting.Common.Modules.Dependency;
using System.Reflection;

namespace Suyaa.Hosting.Common.Modules.Helpers
{
    /// <summary>
    /// 容器扩展
    /// </summary>
    public static class DependencyManagerHelper
    {
        /// <summary>
        /// 添加启动器
        /// </summary>
        /// <param name="dependencyManager"></param>
        /// <param name="tp"></param>
        /// <returns></returns>
        public static IDependencyManager AddModulerStartup(this IDependencyManager dependencyManager, Type? tp)
        {
            if (tp is null) return dependencyManager;
            if (!tp.HasInterface<IModuleStartup>()) return dependencyManager;
            sy.Logger.Info("AddAssemblyModulerStartup " + tp.FullName, "ModulerStartup");
            var obj = Activator.CreateInstance(tp);
            IModuleStartup? startup = (IModuleStartup?)obj;
            startup?.ConfigureDependency(dependencyManager);
            return dependencyManager;
        }

        /// <summary>
        /// 添加模块程序集
        /// </summary>
        /// <param name="dependencyManager"></param>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static IDependencyManager AddAssemblyModuler(this IDependencyManager dependencyManager, Assembly? assembly)
        {
            sy.Logger.Info("AddAssemblyModuler " + assembly?.Location, "ModulerStartup");
            // 遍历所有的IModulerStartup
            var tps = assembly?.GetTypes();
            if (tps != null)
            {
                foreach (var tp in tps)
                {
                    dependencyManager.AddModulerStartup(tp);
                }
            }
            return dependencyManager;
        }

        /// <summary>
        /// 添加控制器
        /// </summary>
        /// <param name="dependencyManager"></param>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static IDependencyManager AddModuler(this IDependencyManager dependencyManager, Assembly assembly)
        {
            return dependencyManager.AddAssemblyModuler(assembly);
        }

        /// <summary>
        /// 添加控制器
        /// </summary>
        /// <param name="dependencyManager"></param>
        /// <returns></returns>
        public static IDependencyManager AddModuler<T>(this IDependencyManager dependencyManager) where T : IModuleStartup
        {
            dependencyManager.AddModulerStartup(typeof(T));
            return dependencyManager;
        }

        /// <summary>
        /// 添加控制器
        /// </summary>
        /// <param name="dependencyManager"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static IDependencyManager AddModulers(this IDependencyManager dependencyManager, IEnumerable<Assembly> assemblies)
        {
            // 遍历所有的程序集
            foreach (var ass in assemblies)
            {
                dependencyManager.AddAssemblyModuler(ass);
            }
            return dependencyManager;
        }
    }
}
