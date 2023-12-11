using Suyaa.Data;
using Suyaa.Data.Dependency;
using Suyaa.Data.Factories;
using Suyaa.Data.Providers;
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
            dependencyManager.Include<EntityModelProvider>();
            // 注册连接描述相关
            dependencyManager.Register<IDbConnectionDescriptorFactory, HostDbConnectionDescriptorFactory>(Lifetimes.Singleton);
            dependencyManager.RegisterTransientImplementations<IDbConnectionDescriptorProvider>();
            dependencyManager.Register<IDbConnectionDescriptorManager, DbConnectionDescriptorManager>(Lifetimes.Transient);
            // 注册作业相关
            dependencyManager.Register<IDbWorkProvider, HostDbWorkProvider>(Lifetimes.Transient);
            dependencyManager.Register<IDbWorkManagerProvider, HostDbWorkManagerProvider>(Lifetimes.Transient);
            dependencyManager.Register<IDbWorkManager, HostDbWorkManager>(Lifetimes.Transient);
            dependencyManager.Register<IDbWork, DbWork>(Lifetimes.Transient);
            // 注册数据仓库
            dependencyManager.Register(typeof(IRepository<,>), typeof(Repository<,>), Lifetimes.Transient);
            dependencyManager.Register<ISqlRepository, SqlRepository>(Lifetimes.Transient);
            // 注册数据库工厂
            dependencyManager.Register<IDbFactory, DbFactory>(Lifetimes.Singleton);
            // 注册实体建模相关
            dependencyManager.Register<IEntityModelFactory, EntityModelFactory>(Lifetimes.Singleton);
            dependencyManager.Register<IEntityModelConventionFactory, HostEntityModelConventionFactory>(Lifetimes.Singleton);
            dependencyManager.RegisterTransientImplementations<IEntityModelProvider>();
            dependencyManager.RegisterTransientImplementations<IEntityModelConvention>();
            // 注册数据库操作供应商
            dependencyManager.Register(typeof(IDbInsertProvider<>), typeof(DbInsertProvider<>), Lifetimes.Transient);
            dependencyManager.Register(typeof(IDbDeleteProvider<>), typeof(DbDeleteProvider<>), Lifetimes.Transient);
            dependencyManager.Register(typeof(IDbUpdateProvider<>), typeof(DbUpdateProvider<>), Lifetimes.Transient);
            dependencyManager.Register(typeof(IDbQueryProvider<>), typeof(DbQueryProvider<>), Lifetimes.Transient);
            return dependencyManager;
        }
    }
}
