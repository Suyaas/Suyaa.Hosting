using Suyaa.Data.Dependency;
using Suyaa.Hosting.Common.Configures.Dependency;
using Suyaa.Hosting.Common.DependencyInjection.Dependency;
using Suyaa.Hosting.Common.DependencyInjection.Helpers;
using Suyaa.Hosting.Data.Configures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Hosting.Data.Factories
{
    /// <summary>
    /// 数据库连接工厂
    /// </summary>
    public sealed class HostDbConnectionDescriptorFactory : IDbConnectionDescriptorFactory
    {
        // 所有连接信息
        private readonly Dictionary<string, IDbConnectionDescriptor> _descriptors;
        private const string KEY_DEFAULT = "default";

        #region 依赖注入

        private readonly IDependencyManager _dependencyManager;

        /// <summary>
        /// 数据库连接工厂
        /// </summary>
        public HostDbConnectionDescriptorFactory(
            IDependencyManager dependencyManager,
            IOptionConfig<DatabaseConfig> databaseConfig
            )
        {
            _dependencyManager = dependencyManager;
            IEnumerable<IDbConnectionDescriptorProvider> providers = _dependencyManager.Resolves<IDbConnectionDescriptorProvider>();
            // 初始化所有连接信息
            _descriptors = new Dictionary<string, IDbConnectionDescriptor>();
            foreach (var provider in providers)
            {
                var descriptors = provider.GetDbConnections();
                foreach (var descriptor in descriptors)
                {
                    if (_descriptors.ContainsKey(descriptor.Name)) continue;
                    _descriptors[descriptor.Name] = descriptor;
                }
            }
            // 设置默认连接
            if (!_descriptors.Any()) throw new NullException<Dictionary<string, IDbConnectionDescriptor>>();
            if (_descriptors.ContainsKey(KEY_DEFAULT))
            {
                DefaultConnection = _descriptors[KEY_DEFAULT];
            }
            else
            {
                DefaultConnection = _descriptors.Values.First();
            }
        }

        #endregion

        /// <summary>
        /// 默认连接
        /// </summary>
        public IDbConnectionDescriptor DefaultConnection { get; }

        /// <summary>
        /// 获取连接信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="NotExistException"></exception>
        public IDbConnectionDescriptor GetConnection(string name)
        {
            if (_descriptors.ContainsKey(name)) throw new NotExistException(name);
            return _descriptors[name];
        }
    }
}
