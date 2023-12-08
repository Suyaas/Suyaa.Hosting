using System;
using System.Reflection;
using Suyaa.Hosting.Common.DependencyInjection.Dependency;

namespace Suyaa.Hosting.Common.DependencyInjection.Helpers
{
    /// <summary>
    /// 依赖管理器助手
    /// </summary>
    public static class DependencyManagerHelper
    {

        #region 注册单个类型

        /// <summary>
        /// 注册一个接口与关联实现类
        /// </summary>
        public static IDependencyManager Register<TService, TImplementation>(this IDependencyManager dependencyManager, Lifetimes lifetimes)
        {
            dependencyManager.Register(typeof(TService), typeof(TImplementation), lifetimes);
            return dependencyManager;
        }

        /// <summary>
        /// 注册一个接口与关联实现类
        /// </summary>
        public static IDependencyManager Register<TService>(this IDependencyManager dependencyManager, Type implementationType, Lifetimes lifetimes)
        {
            dependencyManager.Register(typeof(TService), implementationType, lifetimes);
            return dependencyManager;
        }

        /// <summary>
        /// 注册一个实现类
        /// </summary>
        public static IDependencyManager Register(this IDependencyManager dependencyManager, Type implementationType, Lifetimes lifetimes)
        {
            if (implementationType.IsInterface) return dependencyManager;
            dependencyManager.Register(implementationType, implementationType, lifetimes);
            return dependencyManager;
        }

        /// <summary>
        /// 注册一个实现类
        /// </summary>
        public static IDependencyManager Register<TImplementation>(this IDependencyManager dependencyManager, Lifetimes lifetimes)
        {
            return dependencyManager.Register(typeof(TImplementation), lifetimes);
        }

        /// <summary>
        /// 注册一个实例
        /// </summary>
        public static IDependencyManager RegisterInstance<TService>(this IDependencyManager dependencyManager, object implementationInstance)
        {
            dependencyManager.RegisterInstance(typeof(TService), implementationInstance);
            return dependencyManager;
        }

        /// <summary>
        /// 注册一个实例
        /// </summary>
        public static IDependencyManager RegisterInstance(this IDependencyManager dependencyManager, object implementationInstance)
        {
            dependencyManager.RegisterInstance(implementationInstance.GetType(), implementationInstance);
            return dependencyManager;
        }

        #endregion

        #region 批量注册

        /// <summary>
        /// 按程序集注册接口的所有瞬态实现类(不注册其他接口实现)
        /// </summary>
        /// <param name="dependencyManager"></param>
        /// <param name="serviceType"></param>
        /// <param name="assembly"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IDependencyManager RegisterTransientImplementations(this IDependencyManager dependencyManager, Type serviceType, Assembly assembly, Func<Type, bool> predicate)
        {
            if (!serviceType.IsInterface) return dependencyManager;
            Type[] types = assembly.GetTypes();
            foreach (Type type in types)
            {
                if (type.HasInterface(serviceType))
                {
                    if (!predicate(type)) continue;
                    dependencyManager.Register(serviceType, type, Lifetimes.Transient);
                    dependencyManager.Register(type, type, Lifetimes.Transient);
                }
            }
            return dependencyManager;
        }

        /// <summary>
        /// 按程序集注册接口的所有瞬态实现类(不注册其他接口实现)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dependencyManager"></param>
        /// <param name="assembly"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IDependencyManager RegisterTransientImplementations<T>(this IDependencyManager dependencyManager, Assembly assembly, Func<Type, bool> predicate)
        {
            return dependencyManager.RegisterTransientImplementations(typeof(T), assembly, predicate);
        }

        /// <summary>
        /// 按程序集注册接口的所有瞬态实现类(不注册其他接口实现)
        /// </summary>
        /// <param name="dependencyManager"></param>
        /// <param name="serviceType"></param>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static IDependencyManager RegisterTransientImplementations(this IDependencyManager dependencyManager, Type serviceType, Assembly assembly)
        {
            return dependencyManager.RegisterTransientImplementations(serviceType, assembly, tp => true);
        }

        /// <summary>
        /// 按程序集注册接口的所有瞬态实现类(不注册其他接口实现)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dependencyManager"></param>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static IDependencyManager RegisterTransientImplementations<T>(this IDependencyManager dependencyManager, Assembly assembly)
        {
            return dependencyManager.RegisterTransientImplementations(typeof(T), assembly);
        }

        /// <summary>
        /// 注册接口的所有瞬态实现类(不注册其他接口实现)
        /// </summary>
        /// <param name="dependencyManager"></param>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public static IDependencyManager RegisterTransientImplementations(this IDependencyManager dependencyManager, Type serviceType)
        {
            foreach (Assembly assembly in dependencyManager.Assemblies)
            {
                dependencyManager.RegisterTransientImplementations(serviceType, assembly);
            }
            return dependencyManager;
        }

        /// <summary>
        /// 注册接口的所有瞬态实现类(不注册其他接口实现)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dependencyManager"></param>
        /// <returns></returns>
        public static IDependencyManager RegisterTransientImplementations<T>(this IDependencyManager dependencyManager)
        {
            return dependencyManager.RegisterTransientImplementations(typeof(T));
        }

        #endregion

        #region 全量注册

        // 依赖专用接口
        private static List<Type> dependencyInterfaces = new List<Type> {
            Types.Transient,
            Types.Exclusive,
            Types.Singleton,
        };

        /// <summary>
        /// 注册实现类的所有接口
        /// </summary>
        /// <param name="dependencyManager"></param>
        /// <param name="implementationType"></param>
        /// <param name="lifetime"></param>
        public static IDependencyManager RegisterImplementationInterfaces(this IDependencyManager dependencyManager, Type implementationType, Lifetimes lifetime)
        {
            // 获取所有接口
            var ifcs = implementationType.GetInterfaces();
            foreach (var ifc in ifcs)
            {
                // 过滤类型
                if (dependencyInterfaces.Contains(ifc)) continue;
                // 注册类
                dependencyManager.Register(ifc, implementationType, lifetime);
            }
            return dependencyManager;
        }

        /// <summary>
        /// 按程序集注册接口实现类相关的所有接口
        /// </summary>
        /// <param name="dependencyManager"></param>
        /// <param name="serviceType"></param>
        /// <param name="lifetime"></param>
        /// <param name="assembly"></param>
        /// <param name="predicate"></param>
        public static IDependencyManager RegisterAssemblyImplementationInterfaces(this IDependencyManager dependencyManager, Type serviceType, Lifetimes lifetime, Assembly assembly, Func<Type, bool> predicate)
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
                        // 注册所有接口
                        dependencyManager.RegisterImplementationInterfaces(type, lifetime);
                        dependencyManager.Register(type, lifetime);
                    }
                }
            }
            catch { }
            return dependencyManager;
        }

        /// <summary>
        /// 按程序集注册接口实现类相关的所有接口
        /// </summary>
        /// <param name="dependencyManager"></param>
        /// <param name="lifetime"></param>
        /// <param name="assembly"></param>
        public static IDependencyManager RegisterAssemblyImplementationInterfaces<TService>(this IDependencyManager dependencyManager, Lifetimes lifetime, Assembly assembly)
        {
            return dependencyManager.RegisterAssemblyImplementationInterfaces(typeof(TService), lifetime, assembly, tp => true);
        }

        /// <summary>
        /// 注册接口实现类相关的所有接口
        /// </summary>
        /// <param name="dependencyManager"></param>
        /// <param name="serviceType"></param>
        /// <param name="lifetime"></param>
        /// <param name="predicate"></param>
        public static IDependencyManager RegisterImplementationInterfaces(this IDependencyManager dependencyManager, Type serviceType, Lifetimes lifetime, Func<Type, bool> predicate)
        {
            foreach (Assembly assembly in dependencyManager.Assemblies)
            {
                dependencyManager.RegisterAssemblyImplementationInterfaces(serviceType, lifetime, assembly, predicate);
            }
            return dependencyManager;
        }

        /// <summary>
        /// 注册所有的单例实现
        /// </summary>
        /// <param name="dependencyManager"></param>
        /// <param name="lifetime"></param>
        public static IDependencyManager RegisterImplementationInterfaces<TService>(this IDependencyManager dependencyManager, Lifetimes lifetime)
        {
            return dependencyManager.RegisterImplementationInterfaces(typeof(TService), lifetime, tp => true);
        }

        #endregion

        /// <summary>
        /// 抽取
        /// </summary>
        /// <param name="dependencyManager"></param>
        public static T Resolve<T>(this IDependencyManager dependencyManager)
        {
            return (T)dependencyManager.Resolve(typeof(T));
        }

        /// <summary>
        /// 抽取多个实现
        /// </summary>
        /// <param name="dependencyManager"></param>
        public static List<T> Resolves<T>(this IDependencyManager dependencyManager)
        {
            List<T> list = new List<T>();
            var types = dependencyManager.GetImplementationTypes<T>();
            foreach (var type in types)
            {
                list.Add((T)dependencyManager.Resolve(type));
            }
            return list;
        }

        /// <summary>
        /// 获取服务类获取所有的可实现类
        /// </summary>
        /// <param name="manager"></param>
        /// <returns></returns>
        public static IEnumerable<Type> GetImplementationTypes<T>(this IDependencyManager manager)
        {
            return manager.GetImplementationTypes(typeof(T));
        }

        #region 引入程序集

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

        #endregion 
    }
}
