using Microsoft.EntityFrameworkCore;
using Suyaa.DependencyInjection;
using Suyaa.Hosting.Kernel.Dependency;
using System.Collections.Generic;

namespace Suyaa.Hosting.EFCore
{
    /// <summary>
    /// 仓库供应商
    /// </summary>
    public class RepositoryProvider : IRepositoryProvider, IDependencyTransient
    {

        #region DI注入

        private readonly IDependencyManager _dependencyManager;

        /// <summary>
        /// 仓库供应商
        /// </summary>
        public RepositoryProvider(
            IDependencyManager dependencyManager
            )
        {
            _dependencyManager = dependencyManager;
        }
        #endregion

        //public IQueryable<TClass> Query<TClass>()
        //{
        //    return _dependencyManager.Resolve<>();
        //}
    }
}