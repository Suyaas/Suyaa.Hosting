using Suyaa.Data.Dependency;
using Suyaa.Data.Enums;
using Suyaa.Data.SimpleDbWorks;
using Suyaa.Hosting.Common.DependencyInjection.Dependency;
using Suyaa.Hosting.Common.DependencyInjection.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Hosting.Data.Providers
{
    /// <summary>
    /// 数据库作业供应商
    /// </summary>
    public sealed class HostDbWorkProvider : IDbWorkProvider
    {
        #region 依赖注入

        private readonly IDependencyManager _dependencyManager;

        /// <summary>
        /// 数据库作业供应商
        /// </summary>
        public HostDbWorkProvider(IDependencyManager dependencyManager)
        {
            _dependencyManager = dependencyManager;
        }

        #endregion

        /// <summary>
        /// 创建一个工作者
        /// </summary>
        /// <returns></returns>
        public IDbWork CreateWork(IDbWorkManager dbWorkManager)
        {
            return _dependencyManager.ResolveRequired<IDbWork>();
        }

    }
}
