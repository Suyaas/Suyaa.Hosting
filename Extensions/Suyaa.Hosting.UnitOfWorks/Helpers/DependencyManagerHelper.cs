using Suyaa.Hosting.Common.DependencyInjection;
using Suyaa.Hosting.Common.DependencyInjection.Dependency;
using Suyaa.Hosting.Common.DependencyInjection.Helpers;
using Suyaa.Hosting.UnitOfWork.Dependency;

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
        /// <param name="dependency"></param>
        /// <returns></returns>
        public static IDependencyManager AddUnitOfWork(this IDependencyManager dependency)
        {
            // 注册工作单元
            dependency.Register<IUnitOfWork, UnitOfWork>(Lifetimes.Transient);
            // 注册工作单元管理器
            dependency.Register<IUnitOfWorkManager, UnitOfWorkManager>(Lifetimes.Transient);
            // 注册所有的工作单元供应商
            dependency.RegisterTransientImplementations<IUnitOfWorkProvider>();
            return dependency;
        }
    }
}
