using Suyaa.DependencyInjection;
using Suyaa.EFCore;
using Suyaa.EFCore.Dependency;
using Suyaa.Hosting.Data.Dependency;
using Suyaa.Hosting.EFCore;
using Suyaa.Hosting.EFCore.Dependency;
using Suyaa.Hosting.Kernel.Helpers;

namespace Suyaa.Hosting.Multilingual.Helpers
{
    /// <summary>
    /// 容器扩展
    /// </summary>
    public static partial class DependencyManagerHelper
    {
        /// <summary>
        /// 添加EFCore支持
        /// </summary>
        /// <param name="dependency"></param>
        /// <returns></returns>
        public static IDependencyManager AddEFCore(this IDependencyManager dependency)
        {
            // 使用数据库
            dependency.AddDatabase();
            // 注册所有的数据库上下文
            List<Type> excludeContextTypes = new List<Type>() { typeof(DbDescriptorTypeContext) };
            dependency.RegisterTransients<IDbDescriptorContext>(type => !excludeContextTypes.Contains(type));
            // 注册连接描述工厂
            dependency.Register<IDbConnectionDescriptorFactory, DbConnectionDescriptorFactory>(Lifetimes.Singleton);
            // 注册数据库上下文工厂
            dependency.Register<IDbContextFactory, DbContextFactory>(Lifetimes.Singleton);
            // 注册异步数据库上下文
            dependency.Register<IDbContextAsyncWork, DbContextAsyncWork>(Lifetimes.Transient);
            dependency.Register<IDbContextAsyncProvider, DbContextAsyncProvider>(Lifetimes.Transient);
            dependency.Register<IDbContextAsyncManager, DbContextAsyncManager>(Lifetimes.Transient);
            return dependency;
        }
    }
}
