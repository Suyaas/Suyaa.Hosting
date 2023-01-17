﻿using Suyaa.Microservice.Dependency;
using System.Reflection;

namespace Suyaa.Microservice.Extensions
{
    /// <summary>
    /// 模块启动器扩展
    /// </summary>
    public static class ModulerStartupExtensions
    {
        // 核心服务接口类型
        private readonly static Type _serviceCoreType = typeof(IServiceCore);

        // 添加程序集
        private static void AddModulerAssemblyType(IServiceCollection services, Type? tp)
        {
            egg.Logger.Info("AddModulerAssemblyType " + (tp?.FullName ?? ""), "ModulerStartup");
            var ifs = tp?.GetInterfaces().Where(x => x == typeof(IModuleStartup));
            if (ifs?.Any() ?? false)
            {
                if (tp != null)
                {
                    IModuleStartup? startup = (IModuleStartup?)Activator.CreateInstance(tp);
                    startup?.ConfigureServices(services);
                }
            }
        }

        // 添加程序集
        private static void AddModulerAssembly(IServiceCollection services, Assembly? assembly)
        {
            egg.Logger.Info("AddModulerAssembly " + (assembly?.Location ?? ""), "ModulerStartup");
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
        public static void AddModuler<T>(this IServiceCollection services) where T : IModuleStartup
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

        // 是否为核心服务接口
        private static bool HasServiceCore(Type[] types)
        {
            foreach (var tp in types)
            {
                if (tp == _serviceCoreType)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 添加关联的依赖注入
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static void AddModulerIoc(this IServiceCollection services, Assembly assembly)
        {
            egg.Logger.Info("AddModulerIoc " + (assembly?.Location ?? ""), "ModulerStartup");
            // 遍历所有的IModulerStartup
            //var tps = ass?.GetTypes().Where(d => d.BaseType == typeof(IModulerStartup));
            var tps = assembly?.GetTypes();
            if (tps != null)
            {
                foreach (var tp in tps)
                {
                    // 跳过接口
                    if (tp.IsInterface) continue;
                    // 获取所有接口
                    var ifs = tp?.GetInterfaces();
                    if (ifs is null) continue;
                    if (!HasServiceCore(ifs)) continue;
                    foreach (var ifc in ifs)
                    {
                        // 跳过直接引用IServiceCore接口的类
                        if (ifc == _serviceCoreType) continue;
                        // 添加核心服务类
                        egg.Logger.Info("AddModulerIoc " + (ifc?.FullName ?? "") + " : " + (tp?.FullName ?? ""), "ModulerStartup");
                        if (ifc != null && tp != null) services.AddTransient(ifc, tp);
                    }
                }
            }
        }

        /// <summary>
        /// 添加关联的依赖注入
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static void AddModulerIoc<T>(this IServiceCollection services) where T : IModuleStartup
        {
            services.AddModulerIoc(typeof(T).Assembly);
        }
    }
}
