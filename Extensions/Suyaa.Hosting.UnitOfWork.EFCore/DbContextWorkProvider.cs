using Suyaa.Hosting.Common.DependencyInjection.Dependency;
using Suyaa.Hosting.Common.DependencyInjection.Helpers;
using Suyaa.Hosting.EFCore.Dependency;

namespace Suyaa.Hosting.EFCore
{
    /// <summary>
    /// 数据库上下文异步工作
    /// </summary>
    public class DbContextWorkProvider : IDbContextWorkProvider
    {

        #region DI注入

        private readonly IDependencyManager _dependencyManager;

        /// <summary>
        /// 数据库上下文异步工作
        /// </summary>
        public DbContextWorkProvider(
            IDependencyManager dependencyManager
            )
        {
            _dependencyManager = dependencyManager;
        }

        #endregion

        /// <summary>
        /// 创建作业
        /// </summary>
        /// <returns></returns>
        public IDbContextWork CreateWork()
        {
            return _dependencyManager.ResolveRequired<IDbContextWork>();
        }
    }
}
