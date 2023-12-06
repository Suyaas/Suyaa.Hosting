using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Suyaa.Hosting.Common.DependencyManager.Dependency;
using Suyaa.Hosting.Common.DependencyManager.Helpers;

namespace Suyaa.Hosting.Common.DependencyManager.Helpers
{
    /// <summary>
    /// 依赖管理器助手
    /// </summary>
    public static class DependencyManagerHelper
    {
        #region 按接口注册

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="serviceType"></param>
        /// <param name="implementationType"></param>
        /// <exception cref="DependencyException"></exception>
        public static bool RegisterByInterface(this IDependencyManager manager, Type serviceType, Type implementationType)
        {
            // 是否实现了单例
            if (Types.Singleton.IsAssignableFrom(implementationType))
            {
                manager.Register(serviceType, implementationType, Lifetimes.Singleton);
                return true;
            }
            // 是否实现了瞬态
            if (Types.Transient.IsAssignableFrom(implementationType))
            {
                // 是否实现了独占
                if (Types.Exclusive.IsAssignableFrom(implementationType))
                {
                    manager.Register(serviceType, implementationType, Lifetimes.Exclusive);
                }
                else
                {
                    manager.Register(serviceType, implementationType, Lifetimes.Transient);
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="implementationType"></param>
        /// <exception cref="DependencyException"></exception>
        public static bool RegisterByInterface(this IDependencyManager manager, Type implementationType)
        {
            // 是否实现了单例
            if (Types.Singleton.IsAssignableFrom(implementationType))
            {
                manager.Register(implementationType, Lifetimes.Singleton);
                return true;
            }
            // 是否实现了瞬态
            if (Types.Transient.IsAssignableFrom(implementationType))
            {
                manager.Register(implementationType, Lifetimes.Transient);
                return true;
            }
            return false;
        }

        #endregion

        #region 自动判断接口注册

        /// <summary>
        /// 自动判断生命周期并注册
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="serviceType"></param>
        /// <param name="implementationType"></param>
        public static bool RegisterAuto(this IDependencyManager manager, Type serviceType, Type implementationType)
        {
            // 优先按接口注册
            if (manager.RegisterByInterface(serviceType, implementationType)) return true;
            return false;
        }

        /// <summary>
        /// 自动判断生命周期并注册
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="implementationType"></param>
        public static bool RegisterAuto(this IDependencyManager manager, Type implementationType)
        {
            // 优先按接口注册
            if (manager.RegisterByInterface(implementationType)) return true;
            return false;
        }

        #endregion

        /// <summary>
        /// 注册
        /// </summary>
        public static void Register<TService, TImplementation>(this IDependencyManager manager, Lifetimes lifetimes)
        {
            manager.Register(typeof(TService), typeof(TImplementation), lifetimes);
        }

        /// <summary>
        /// 注册
        /// </summary>
        public static void Register<TService>(this IDependencyManager manager, Type type, Lifetimes lifetimes)
        {
            manager.Register(typeof(TService), type, lifetimes);
        }

        /// <summary>
        /// 注册
        /// </summary>
        public static void Register<T>(this IDependencyManager manager, object implementationInstance)
        {
            manager.Register(typeof(T), implementationInstance);
        }

        /// <summary>
        /// 注册
        /// </summary>
        public static void Register(this IDependencyManager manager, object implementationInstance)
        {
            manager.Register(implementationInstance.GetType(), implementationInstance);
        }

        /// <summary>
        /// 注册所有接口到单例
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="type"></param>
        public static void RegisterSingletonInterfaces(this IDependencyManager manager, Type type)
        {
            // 过滤类型
            List<Type> interfaces = new List<Type> {
                Types.Transient,
                Types.Exclusive,
                Types.Singleton,
            };
            // 获取所有接口
            var ifs = type.GetInterfaces();
            foreach (var ifc in ifs)
            {
                // 过滤类型
                if (interfaces.Contains(ifc)) continue;
                // 注册类
                manager.Register(ifc, type, Lifetimes.Singleton);
            }
        }

        /// <summary>
        /// 注册所有接口到瞬态
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="type"></param>
        public static void RegisterTransientInterfaces(this IDependencyManager manager, Type type)
        {
            // 过滤类型
            List<Type> interfaces = new List<Type> {
                Types.Transient,
                Types.Exclusive,
                Types.Singleton,
            };
            // 获取所有接口
            var ifs = type.GetInterfaces();
            foreach (var ifc in ifs)
            {
                // 过滤类型
                if (interfaces.Contains(ifc)) continue;
                // 注册类
                manager.Register(ifc, type, Lifetimes.Transient);
            }
        }

        /// <summary>
        /// 注册所有接口到独占瞬态
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="type"></param>
        public static void RegisterExclusiveInterfaces(this IDependencyManager manager, Type type)
        {
            // 过滤类型
            List<Type> interfaces = new List<Type> {
                Types.Transient,
                Types.Exclusive,
                Types.Singleton,
            };
            // 获取所有接口
            var ifs = type.GetInterfaces();
            foreach (var ifc in ifs)
            {
                // 过滤类型
                if (interfaces.Contains(ifc)) continue;
                // 注册类
                manager.Register(ifc, type, Lifetimes.Exclusive);
            }
        }

        /// <summary>
        /// 按程序集注册
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="assembly"></param>
        public static void RegisterAssembly(this IDependencyManager manager, Assembly assembly)
        {
            try
            {
                Type[] types = assembly.GetTypes();
                foreach (Type type in types)
                {
                    // 跳过所有的接口
                    if (type.IsInterface) continue;
                    // 条码抽象类
                    if (type.IsAbstract) continue;
                    // 是否实现了单例
                    if (Types.Singleton.IsAssignableFrom(type))
                    {
                        // 注册所有单例接口
                        manager.RegisterSingletonInterfaces(type);
                        manager.Register(type, type, Lifetimes.Singleton);
                        continue;
                    }
                    // 是否实现了瞬态
                    if (Types.Transient.IsAssignableFrom(type))
                    {
                        if (Types.Exclusive.IsAssignableFrom(type))
                        {
                            // 注册所有独占接口
                            manager.RegisterSingletonInterfaces(type);
                            manager.Register(type, type, Lifetimes.Exclusive);
                        }
                        else
                        {
                            // 注册所有瞬态接口
                            manager.RegisterTransientInterfaces(type);
                            manager.Register(type, type, Lifetimes.Transient);
                        }
                        continue;
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// 按程序集注册接口实现
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="assembly"></param>
        /// <param name="predicate"></param>
        public static void RegisterAssemblyTransients<TService>(this IDependencyManager manager, Assembly assembly, Func<Type, bool> predicate)
        {
            manager.RegisterAssemblyTransients(typeof(TService), assembly, predicate);
        }

        /// <summary>
        /// 按程序集注册接口实现
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="serviceType"></param>
        /// <param name="assembly"></param>
        /// <param name="predicate"></param>
        public static void RegisterAssemblyTransients(this IDependencyManager manager, Type serviceType, Assembly assembly, Func<Type, bool> predicate)
        {
            try
            {
                Type[] types = assembly.GetTypes();
                foreach (Type type in types)
                {
                    // 跳过所有的接口
                    if (type.IsInterface) continue;
                    // 条码抽象类
                    if (type.IsAbstract) continue;
                    if (type.HasInterface(serviceType))
                    {
                        if (!predicate(type)) continue;
                        manager.Register(serviceType, type, Lifetimes.Transient);
                        manager.Register(type, type, Lifetimes.Transient);
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// 注册所有类
        /// </summary>
        /// <param name="dependency"></param>
        public static void RegisterAll(this IDependencyManager dependency)
        {
            //Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly assembly in dependency.Assemblies)
            {
                dependency.RegisterAssembly(assembly);
            }
        }

        /// <summary>
        /// 注册所有的接口实现
        /// </summary>
        /// <param name="dependency"></param>
        /// <param name="serviceType"></param>
        public static void RegisterTransients(this IDependencyManager dependency, Type serviceType)
        {
            //Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly assembly in dependency.Assemblies)
            {
                dependency.RegisterAssemblyTransients(serviceType, assembly, type => true);
            }
        }

        /// <summary>
        /// 注册所有的接口实现
        /// </summary>
        /// <param name="dependency"></param>
        /// <param name="serviceType"></param>
        /// <param name="predicate"></param>
        public static void RegisterTransients(this IDependencyManager dependency, Type serviceType, Func<Type, bool> predicate)
        {
            //Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly assembly in dependency.Assemblies)
            {
                dependency.RegisterAssemblyTransients(serviceType, assembly, predicate);
            }
        }

        /// <summary>
        /// 注册所有的接口实现
        /// </summary>
        /// <param name="manager"></param>
        public static void RegisterTransients<TService>(this IDependencyManager manager)
        {
            manager.RegisterTransients(typeof(TService));
        }

        /// <summary>
        /// 注册所有的接口实现
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="predicate"></param>
        public static void RegisterTransients<TService>(this IDependencyManager manager, Func<Type, bool> predicate)
        {
            manager.RegisterTransients(typeof(TService), predicate);
        }

        /// <summary>
        /// 抽取
        /// </summary>
        /// <param name="manager"></param>
        public static T Resolve<T>(this IDependencyManager manager)
        {
            return manager.Resolve(typeof(T));
        }

        /// <summary>
        /// 获取服务类获取所有的可实现类
        /// </summary>
        /// <param name="manager"></param>
        /// <returns></returns>
        public static List<Type> GetResolveTypes<T>(this IDependencyManager manager)
        {
            return manager.GetResolveTypes(typeof(T));
        }

        /// <summary>
        /// 引用程序集
        /// </summary>
        /// <param name="dependency"></param>
        /// <param name="assemblies"></param>
        public static IDependencyManager Includes(this IDependencyManager dependency, IEnumerable<Assembly> assemblies)
        {
            foreach (var assembly in assemblies) dependency.Include(assembly);
            return dependency;
        }

        /// <summary>
        /// 导入程序集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IDependencyManager Include<T>(this IDependencyManager dependency) where T : class
        {
            dependency.Include(typeof(T).Assembly);
            return dependency;
        }
    }
}
