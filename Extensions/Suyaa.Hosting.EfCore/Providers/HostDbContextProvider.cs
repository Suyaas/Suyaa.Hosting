using Suyaa.EFCore.Dependency;
using Suyaa.Hosting.Common.DependencyInjection.Dependency;
using Suyaa.Hosting.Common.DependencyInjection.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Hosting.EfCore.Providers
{
    /// <summary>
    /// 数据库上下文供应商
    /// </summary>
    public sealed class HostDbContextProvider : IDbContextProvider
    {
        private readonly IEnumerable<IDescriptorDbContext> _dbContexts;
        private readonly IDependencyManager _dependencyManager;

        /// <summary>
        /// 数据库上下文供应商
        /// </summary>
        public HostDbContextProvider(
            IDependencyManager dependencyManager
            )
        {
            _dependencyManager = dependencyManager;
            _dbContexts = _dependencyManager.Resolves<IDescriptorDbContext>();
        }

        /// <summary>
        /// 获取数据库上下文集合
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IDescriptorDbContext> GetDbContexts()
        {
            return _dbContexts;
        }
    }
}
