using Suyaa.Data.Dependency;
using Suyaa.Hosting.Common.DependencyInjection;
using Suyaa.Hosting.Common.DependencyInjection.Dependency;
using Suyaa.Hosting.Common.DependencyInjection.Helpers;
using Suyaa.Hosting.Data;
using Suyaa.Hosting.Data.Dependency;
using Suyaa.Hosting.Data.Helpers;
using Suyaa.Hosting.UnitOfWork.Data.Providers;
using Suyaa.Hosting.UnitOfWork.Dependency;
using Suyaa.Hosting.UnitOfWork.Helpers;

namespace Suyaa.Hosting.Multilingual.Helpers
{
    /// <summary>
    /// 容器扩展
    /// </summary>
    public static partial class DependencyManagerHelper
    {
        /// <summary>
        /// 添加数据库工作单元的支持
        /// </summary>
        /// <param name="dependencyManager"></param>
        /// <returns></returns>
        public static IDependencyManager AddDbUnitOfWork(this IDependencyManager dependencyManager)
        {
            // 注册工作单元支持
            dependencyManager.AddUnitOfWork();
            // 注册数据库支持
            dependencyManager.AddData();
            // 注册程序集
            dependencyManager.Include<DataUnitOfWorkProvider>();
            // 注册工作单元供应商
            dependencyManager.Register<IUnitOfWorkProvider, DataUnitOfWorkProvider>(Lifetimes.Transient);
            dependencyManager.Register<DataUnitOfWorkProvider>(Lifetimes.Transient);
            return dependencyManager;
        }
    }
}
