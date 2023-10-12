using Suyaa.DependencyInjection;
using Suyaa.Hosting.EFCore;
using Suyaa.Hosting.EFCore.Dependency;

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
            // 注册连接描述工厂
            dependency.Register<IDbConnectionDescriptorFactory, DbConnectionDescriptorFactory>(Lifetimes.Singleton);
            // 注册所有的数据库上下文
            dependency.RegisterTransients<IHostDbContext>();
            return dependency;
        }
    }
}
