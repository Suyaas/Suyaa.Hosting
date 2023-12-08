using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Suyaa.Hosting.Common.DependencyInjection;

namespace Suyaa.Hosting.Common.DependencyInjection.Dependency
{
    /// <summary>
    /// 依赖管理器
    /// </summary>
    public interface IDependencyManager
    {
        /// <summary>
        /// 包含程序集
        /// </summary>
        IEnumerable<Assembly> Assemblies { get; }

        /// <summary>
        /// 引入程序集
        /// </summary>
        /// <param name="assembly"></param>
        void Include(Assembly assembly);

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="serviceType"></param>
        /// <param name="implementationType"></param>
        /// <param name="lifeCycle"></param>
        void Register(Type serviceType, Type implementationType, Lifetimes lifeCycle);

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="serviceType"></param>
        /// <param name="implementationInstance"></param>
        void RegisterInstance(Type serviceType, object implementationInstance);

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="serviceType"></param>
        void Remove(Type serviceType);

        /// <summary>
        /// 抽取
        /// </summary>
        /// <param name="type"></param>
        object? Resolve(Type type);

        /// <summary>
        /// 获取服务类获取所有的可实现类
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        IEnumerable<Type> GetImplementationTypes(Type serviceType);
    }
}
