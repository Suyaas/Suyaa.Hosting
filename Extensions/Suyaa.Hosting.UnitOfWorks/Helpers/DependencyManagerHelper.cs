using Suyaa.Hosting.Common.ActionFilters.Dependency;
using Suyaa.Hosting.Common.DependencyInjection;
using Suyaa.Hosting.Common.DependencyInjection.Dependency;
using Suyaa.Hosting.Common.DependencyInjection.Helpers;
using Suyaa.Hosting.UnitOfWork.Dependency;
using Suyaa.Hosting.UnitOfWork.Providers;

namespace Suyaa.Hosting.UnitOfWork.Helpers
{
    /// <summary>
    /// 容器扩展
    /// </summary>
    public static partial class DependencyManagerHelper
    {
        /// <summary>
        /// 添加多语言支持
        /// </summary>
        /// <param name="dependencyManager"></param>
        /// <returns></returns>
        public static IDependencyManager AddUnitOfWork(this IDependencyManager dependencyManager)
        {
            // 注册模块
            dependencyManager.Include<UnitOfWork>();
            // 注册工作单元
            dependencyManager.Register<IUnitOfWork, UnitOfWork>(Lifetimes.Transient);
            // 注册工作单元管理器
            dependencyManager.Register<IUnitOfWorkManager, UnitOfWorkManager>(Lifetimes.Transient);
            // 注册所有的工作单元供应商
            dependencyManager.RegisterTransientImplementations<IUnitOfWorkProvider>();
            // 注册切片供应商
            dependencyManager.Register<IActionFilterProvider, UnitOfWorkActionFilterProvider>(Lifetimes.Transient);
            dependencyManager.Register<UnitOfWorkActionFilterProvider>(Lifetimes.Transient);
            return dependencyManager;
        }
    }
}
