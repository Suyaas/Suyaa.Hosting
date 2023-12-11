using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Suyaa.Hosting.Common.DependencyInjection.Dependency;
using System.Reflection;

namespace Suyaa.Hosting.Common.DependencyInjection
{
    /// <summary>
    /// 依赖控制器
    /// </summary>
    public class DependencyManager : IDependencyManager
    {
        // DI容器
        private readonly IServiceCollection _services;
        private readonly Type _exclusive = typeof(IDependencyExclusive);
        private readonly List<Assembly> _assemblies;
        // 异步上下文
        private readonly AsyncLocal<ServiceProviderWrapper> _asyncLocal = new AsyncLocal<ServiceProviderWrapper>();

        #region 构造函数

        /// <summary>
        /// 依赖控制器
        /// </summary>
        public DependencyManager()
        {
            _services = new ServiceCollection();
            _services.AddSingleton<IDependencyManager>(this);
            _assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
        }

        /// <summary>
        /// 依赖控制器
        /// </summary>
        public DependencyManager(IServiceCollection services)
        {
            _services = services;
            _services.AddSingleton<IDependencyManager>(this);
            _assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
        }

        #endregion

        #region 静态方法

        // 当前依赖管理器
        private static IDependencyManager? _dependencyManager;

        /// <summary>
        /// 获取当前依赖管理器
        /// </summary>
        /// <returns></returns>
        public static IDependencyManager? GetCurrent()
        {
            return _dependencyManager;
        }

        /// <summary>
        /// 创建一个依赖管理器
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IDependencyManager Create(IServiceCollection services)
        {
            _dependencyManager = new DependencyManager(services);
            return _dependencyManager;
        }
        #endregion

        /// <summary>
        /// 服务集合
        /// </summary>
        public IServiceCollection Services => _services;

        /// <summary>
        /// 包含程序集
        /// </summary>
        public IEnumerable<Assembly> Assemblies => _assemblies;

        #region 程序集操作

        /// <summary>
        /// 引用程序集
        /// </summary>
        /// <param name="assembly"></param>
        public void Include(Assembly assembly)
        {
            if (_assemblies.Contains(assembly)) return;
            _assemblies.Add(assembly);
        }

        #endregion

        #region 注册

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="implementationType"></param>
        /// <param name="lifeCycle"></param>
        public void Register(Type implementationType, Lifetimes lifeCycle)
        {
            // 过滤接口
            if (implementationType.IsInterface) return;
            // 过滤抽象类
            if (implementationType.IsAbstract) return;
            // 去重校验
            if (GetImplementationTypes(implementationType).Contains(implementationType)) return;
            switch (lifeCycle)
            {
                case Lifetimes.Singleton:
                    _services.AddSingleton(implementationType);
                    break;
                default:
                    _services.AddTransient(implementationType);
                    break;
            }
        }

        // 以兼容独占的方式注册瞬态
        private void RegisterTransient(Type serviceType, Type implementationType)
        {
            // 获取所有可抽取的类型
            var types = GetImplementationTypes(serviceType);
            // 去重校验
            if (types.Contains(implementationType)) return;
            // 判断已经有存在的实现，如果有，独占注册只允许一个
            foreach (var type in types)
            {
                // 判断是否存在独占
                if (_exclusive.IsAssignableFrom(type))
                {
                    // 跳过有独占的注册
                    return;
                }
            }
            // 添加瞬态注册
            _services.AddTransient(serviceType, implementationType);
        }

        // 以兼容独占的方式注册瞬态
        private void RegisterExclusive(Type serviceType, Type implementationType)
        {
            // 获取所有可抽取的类型
            var types = GetImplementationTypes(serviceType);
            // 去重校验
            if (types.Contains(implementationType)) return;
            // 判断已经有存在的实现，如果有，独占注册只允许一个
            foreach (var type in types)
            {
                // 判断是否存在独占
                if (_exclusive.IsAssignableFrom(type))
                {
                    // 抛出独占异常
                    throw new DependencyException(implementationType, "Exists", $"Exclusive type already exists");
                }
            }
            // 移除所有现有的注册
            _services.RemoveAll(serviceType);
            // 添加瞬态注册
            _services.AddTransient(serviceType, implementationType);
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="serviceType"></param>
        /// <param name="implementationType"></param>
        /// <param name="lifeCycle"></param>
        public void Register(Type serviceType, Type implementationType, Lifetimes lifeCycle)
        {
            // 过滤接口
            if (implementationType.IsInterface) return;
            // 过滤抽象类
            if (implementationType.IsAbstract) return;
            // 去重校验
            if (GetImplementationTypes(serviceType).Contains(implementationType)) return;
            switch (lifeCycle)
            {
                case Lifetimes.Singleton:
                    _services.AddSingleton(serviceType, implementationType);
                    break;
                case Lifetimes.Exclusive:
                    RegisterExclusive(serviceType, implementationType);
                    break;
                default:
                    RegisterTransient(serviceType, implementationType);
                    break;
            }
        }

        /// <summary>
        /// 注册实例
        /// </summary>
        /// <param name="serviceType"></param>
        /// <param name="implementationInstance"></param>
        public void RegisterInstance(Type serviceType, object implementationInstance)
        {
            _services.AddSingleton(serviceType, implementationInstance);
        }

        #endregion

        /// <summary>
        /// 移除注入
        /// </summary>
        /// <param name="serviceType"></param>
        public void Remove(Type serviceType)
        {
            _services.RemoveAll(serviceType);
        }

        /// <summary>
        /// 抽取
        /// </summary>
        /// <param name="type"></param>
        public object? Resolve(Type type)
        {
            // 处理异步上下文
            if (_asyncLocal.Value is null)
            {
                lock (_asyncLocal)
                {
                    _asyncLocal.Value = new ServiceProviderWrapper(_services.BuildServiceProvider());
                }
            }
            var provider = _asyncLocal.Value.ServiceProvider;
            var obj = provider.GetService(type);
            if (obj is null) throw new DependencyException(type);
            return obj;
        }

        /// <summary>
        /// 获取服务类获取所有的可实现类
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public IEnumerable<Type> GetImplementationTypes(Type serviceType)
        {
            List<Type> list = new List<Type>();
            var types = _services.Where(d => d.ServiceType == serviceType).Select(d => d.ImplementationType).ToList();
            if (types is null) return list;
            foreach (var type in types)
            {
                if (type is null) continue;
                list.Add(type);
            }
            return list;
        }
    }
}