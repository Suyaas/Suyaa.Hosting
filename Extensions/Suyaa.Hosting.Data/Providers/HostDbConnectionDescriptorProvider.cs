using Suyaa.Data;
using Suyaa.Data.Dependency;
using Suyaa.Hosting.Common.Configures.Dependency;
using Suyaa.Hosting.Common.DependencyInjection.Dependency;
using Suyaa.Hosting.Data.Configures;
using Suyaa.Hosting.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Hosting.Data.Providers
{
    /// <summary>
    /// 数据库连接描述供应商
    /// </summary>
    public sealed class HostDbConnectionDescriptorProvider : IDbConnectionDescriptorProvider
    {

        // 所有连接信息
        private readonly List<IDbConnectionDescriptor> _descriptors;

        #region 依赖注入

        /// <summary>
        /// 数据库连接描述供应商
        /// </summary>
        public HostDbConnectionDescriptorProvider(
            IOptionConfig<DatabaseConfig> databaseConfig
            )
        {
            _descriptors = new List<IDbConnectionDescriptor>();
            var config = databaseConfig.CurrentValue;
            foreach (var connection in config.Connections)
            {
                var descriptor = new DbConnectionDescriptor(connection.Key, connection.Value.GetDatabaseType(), connection.Value.ConnectionString);
                _descriptors.Add(descriptor);
            }
        }

        #endregion

        /// <summary>
        /// 获取数据库连接描述集合
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IDbConnectionDescriptor> GetDbConnections()
        {
            return _descriptors;
        }
    }
}
