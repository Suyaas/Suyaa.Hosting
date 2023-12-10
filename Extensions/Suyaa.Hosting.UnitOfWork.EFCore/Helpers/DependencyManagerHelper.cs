using Suyaa.EFCore.Contexts;
using Suyaa.EFCore.Dependency;
using Suyaa.Hosting.Common.DependencyInjection;
using Suyaa.Hosting.Common.DependencyInjection.Dependency;
using Suyaa.Hosting.Common.DependencyInjection.Helpers;
using Suyaa.Hosting.EFCore.Dependency;
using Suyaa.Hosting.Multilingual.Helpers;
using Suyaa.Hosting.UnitOfWork.EFCore;
using Suyaa.Hosting.UnitOfWork.EFCore.Dependency;
using IDbSetFactory = Suyaa.Hosting.UnitOfWork.EFCore.Dependency.IDbSetFactory;
using IDbSetProvider = Suyaa.Hosting.UnitOfWork.EFCore.Dependency.IDbSetProvider;

namespace Suyaa.Hosting.EFCore.Helpers
{
    /// <summary>
    /// 容器扩展
    /// </summary>
    public static partial class DependencyManagerHelper
    {
        /// <summary>
        /// 添加EFCore支持
        /// </summary>
        /// <param name="dependencyManager"></param>
        /// <returns></returns>
        public static IDependencyManager AddEFCoreUnitOfWork(this IDependencyManager dependencyManager)
        {
            // 注册程序集
            dependencyManager.Include<DbSetFactory>();
            // 使用数据库
            dependencyManager.AddDbUnitOfWork();
            // 注册所有的带描述数据库上下文
            List<Type> excludeContextTypes = new List<Type>() { typeof(DescriptorTypeDbContext) };
            dependencyManager.RegisterTransientImplementations(typeof(IDescriptorDbContext), tp => !excludeContextTypes.Contains(tp));
            // 注册所有的 DbSet 供应商
            dependencyManager.RegisterTransientImplementations<IDbSetProvider>();
            // 注册数据库 DbSet 工厂
            dependencyManager.Register<IDbSetFactory, DbSetFactory>(Lifetimes.Singleton);
            // 注册异步数据库上下文
            dependencyManager.Register<IDbContextWork, DbContextWork>(Lifetimes.Transient);
            dependencyManager.Register<IDbContextWorkProvider, DbContextWorkProvider>(Lifetimes.Transient);
            dependencyManager.Register<IDbContextWorkManager, DbContextWorkManager>(Lifetimes.Transient);
            return dependencyManager;
        }
    }
}
