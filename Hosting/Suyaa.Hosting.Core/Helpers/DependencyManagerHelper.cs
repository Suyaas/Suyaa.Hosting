﻿using Suyaa.Hosting.Common.DependencyInjection;
using Suyaa.Hosting.Common.DependencyInjection.Dependency;
using Suyaa.Hosting.Common.DependencyInjection.Helpers;
using Suyaa.Hosting.Common.Modules.Dependency;
using Suyaa.Hosting.Core.Dependency;
using System.Reflection;

namespace Suyaa.Hosting.Core.Helpers
{
    /// <summary>
    /// 容器扩展
    /// </summary>
    public static class DependencyManagerHelper
    {
        // 核心服务接口类型
        private readonly static Type _serviceCoreType = typeof(IDomainServiceCore);

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

        /// <summary>
        /// 添加启动器
        /// </summary>
        /// <param name="dependencyManager"></param>
        /// <param name="tp"></param>
        /// <returns></returns>
        public static IDependencyManager AddModulerStartup(this IDependencyManager dependencyManager, Type? tp)
        {
            if (tp is null) return dependencyManager;
            if (!HasInterface<IModuleStartup>(tp)) return dependencyManager;
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

        /// <summary>
        /// 添加关联的服务依赖
        /// </summary>
        /// <param name="dependency"></param>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static IDependencyManager AddModulerCores(this IDependencyManager dependency, Assembly assembly)
        {
            sy.Logger.Info("AddModulerCores " + (assembly.Location ?? ""), "ModulerStartup");
            // 注册程序集
            dependency.RegisterAssemblyImplementationInterfaces<IDomainServiceCore>(Lifetimes.Transient, assembly);
            return dependency;
        }

        /// <summary>
        /// 添加关联的依赖注入
        /// </summary>
        /// <param name="dependency"></param>
        /// <returns></returns>
        public static IDependencyManager AddModulerCores<T>(this IDependencyManager dependency) where T : IModuleStartup
        {
            dependency.AddModulerCores(typeof(T).Assembly);
            return dependency;
        }
    }
}
