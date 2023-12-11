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
    /// 主机数据库作业管理者供应商
    /// </summary>
    public sealed class HostDbWorkManagerProvider : IDbWorkManagerProvider
    {
        private readonly IDependencyManager _dependencyManager;

        /// <summary>
        /// 主机数据库作业管理者供应商
        /// </summary>
        public HostDbWorkManagerProvider(
            IDependencyManager dependencyManager
            )
        {
            _dependencyManager = dependencyManager;
        }

        /// <summary>
        /// 创建一个工作者管理器
        /// </summary>
        /// <returns></returns>
        public IDbWorkManager CreateManager()
        {
            return _dependencyManager.ResolveRequired<IDbWorkManager>();
        }
    }
}
