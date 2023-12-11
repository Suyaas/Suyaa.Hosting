using Suyaa.Data;
using Suyaa.Data.Dependency;
using Suyaa.Data.Providers;
using Suyaa.EFCore.Contexts;
using Suyaa.EFCore.Dependency;
using Suyaa.EFCore.Factories;
using Suyaa.EFCore.Providers;
using Suyaa.Hosting.Common.DependencyInjection;
using Suyaa.Hosting.Common.DependencyInjection.Dependency;
using Suyaa.Hosting.Common.DependencyInjection.Helpers;
using Suyaa.Hosting.Data.Helpers;
using Suyaa.Hosting.EfCore.Factories;

namespace Suyaa.Hosting.EfCore.Helpers
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
        public static IDependencyManager AddEfCore(this IDependencyManager dependencyManager)
        {
            // 注册程序集
            dependencyManager.Include<DbContextFacotry>();
            dependencyManager.Include<HostDbContextFacotry>();
            // 使用数据库
            dependencyManager.AddData();
            // 注册作业相关
            dependencyManager.Remove<IDbWork>();
            dependencyManager.Register<IDbWork, EfCoreWork>(Lifetimes.Transient);
            //List<Type> excludeContextTypes = new List<Type>() { typeof(DescriptorTypeDbContext) };
            //dependencyManager.RegisterTransientImplementations(typeof(IDescriptorDbContext), tp => !excludeContextTypes.Contains(tp));
            //// 注册所有的 DbSet 供应商
            //dependencyManager.RegisterTransientImplementations<IEntityModelProvider>();
            // 注册异步数据库上下文
            dependencyManager.Register<IDbContextFactory, HostDbContextFacotry>(Lifetimes.Singleton);
            dependencyManager.RegisterTransientImplementations<IDbContextProvider>();
            List<Type> excludeContextTypes = new List<Type>() { typeof(DescriptorTypeDbContext) };
            dependencyManager.RegisterTransientImplementations(typeof(IDescriptorDbContext), tp => !excludeContextTypes.Contains(tp));
            //dependencyManager.Register<IDbContextWork, DbContextWork>(Lifetimes.Transient);
            //dependencyManager.Register<IDbContextWorkProvider, DbContextWorkProvider>(Lifetimes.Transient);
            //dependencyManager.Register<IDbContextWorkManager, DbContextWorkManager>(Lifetimes.Transient);
            // 注册数据库操作供应商
            dependencyManager.Remove(typeof(IDbInsertProvider<>));
            dependencyManager.Register(typeof(IDbInsertProvider<>), typeof(EfCoreInsertProvider<>), Lifetimes.Transient);
            dependencyManager.Remove(typeof(IDbDeleteProvider<>));
            dependencyManager.Register(typeof(IDbDeleteProvider<>), typeof(EfCoreDeleteProvider<>), Lifetimes.Transient);
            dependencyManager.Remove(typeof(IDbUpdateProvider<>));
            dependencyManager.Register(typeof(IDbUpdateProvider<>), typeof(EfCoreUpdateProvider<>), Lifetimes.Transient);
            dependencyManager.Remove(typeof(IDbQueryProvider<>));
            dependencyManager.Register(typeof(IDbQueryProvider<>), typeof(EfCoreQueryProvider<>), Lifetimes.Transient);
            return dependencyManager;
        }
    }
}
