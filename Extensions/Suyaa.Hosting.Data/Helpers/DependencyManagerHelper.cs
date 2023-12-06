using Suyaa.Hosting.Common.DependencyManager;
using Suyaa.Hosting.Common.DependencyManager.Dependency;
using Suyaa.Hosting.Data;
using Suyaa.Hosting.Data.Dependency;

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
        public static IDependencyManager AddDatabase(this IDependencyManager dependency)
        {
            // 注册程序集
            dependency.Include(typeof(Repository<,>).Assembly);
            // 注册仓库
            dependency.Register(typeof(IRepository<,>), typeof(Repository<,>), Lifetimes.Transient);
            // 注册所有的供应商
            dependency.RegisterTransients(typeof(IDbQueryProvider<,>));
            dependency.RegisterTransients(typeof(IDbInsertProvider<,>));
            dependency.RegisterTransients(typeof(IDbUpdateProvider<,>));
            dependency.RegisterTransients(typeof(IDbDeleteProvider<,>));
            return dependency;
        }
    }
}
