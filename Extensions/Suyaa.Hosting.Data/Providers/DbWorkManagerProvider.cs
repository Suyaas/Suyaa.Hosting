using Suyaa.Data.Dependency;
using Suyaa.Data.Enums;
using Suyaa.Data.Helpers;
using Suyaa.Hosting.Common.DependencyInjection.Dependency;
using Suyaa.Hosting.Common.DependencyInjection.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Suyaa.Data.SimpleDbWorks
{
    /// <summary>
    /// 简单的数据库供应商
    /// </summary>
    public sealed class DbWorkManagerProvider : IDbWorkManagerProvider
    {
        private readonly IDependencyManager _dependencyManager;

        /// <summary>
        /// 简单的数据库工作者供应商
        /// </summary>
        public DbWorkManagerProvider(
            IDependencyManager dependencyManager
            )
        {
            _dependencyManager = dependencyManager;
        }

        /// <summary>
        /// 创建一个工作者管理器
        /// </summary>
        /// <param name="dbConnectionDescriptor"></param>
        /// <returns></returns>
        public IDbWorkManager CreateManager(DbConnectionDescriptor dbConnectionDescriptor)
        {
            return _dependencyManager.ResolveRequired<IDbWorkManager>();
        }
    }
}
