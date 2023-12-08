using Suyaa.Data.Dependency;
using Suyaa.Data.SimpleDbWorks;
using Suyaa.Hosting.Common.DependencyInjection;
using Suyaa.Hosting.Common.DependencyInjection.Dependency;
using Suyaa.Hosting.Common.DependencyInjection.Helpers;
using Suyaa.Hosting.Data.Entities;
using Suyaa.Hosting.Data.Factories;
using Suyaa.Hosting.Data.Managers;
using Suyaa.Hosting.Data.Providers;

namespace Suyaa.Hosting.Data.Helpers
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
        public static IDependencyManager AddData(this IDependencyManager dependencyManager)
        {
            // 注册程序集
            dependencyManager.Include<StandardEntity>();
            // 注册所有的连接描述供应商
            dependencyManager.RegisterTransientImplementations<IDbConnectionDescriptorProvider>();
            // 注册作业供应商
            dependencyManager.Register<IDbWorkProvider, DbWorkProvider>(Lifetimes.Transient);
            // 注册作业管理器供应商
            dependencyManager.Register<IDbWorkManagerProvider, DbWorkManagerProvider>(Lifetimes.Transient);
            // 注册作业管理器
            dependencyManager.Register<IDbWorkManager, DbWorkManager>(Lifetimes.Transient);
            // 注册作业
            dependencyManager.Register<IDbWork, SimpleDbWork>(Lifetimes.Transient);
            // 注册数据仓库
            dependencyManager.Register(typeof(IRepository<,>), typeof(SimpleRepository<,>), Lifetimes.Transient);
            // 注册数据库工厂
            dependencyManager.Register<IDbFactory, DbFactory>(Lifetimes.Singleton);
            // 注册数据库连接描述工厂
            dependencyManager.Register<IDbConnectionDescriptorFactory, DbConnectionDescriptorFactory>(Lifetimes.Singleton);
            return dependencyManager;
        }
    }
}
