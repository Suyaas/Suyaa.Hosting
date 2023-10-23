using Microsoft.Extensions.Configuration;
using Suyaa.Data;
using Suyaa.Hosting.EFCore.Dependency;
using Suyaa.Hosting.Kernel;

namespace Suyaa.Hosting.EFCore
{
    /// <summary>
    /// 主机数据库上下文配置工厂
    /// </summary>
    public class DbConnectionDescriptorFactory : IDbConnectionDescriptorFactory
    {

        #region DI注入

        private readonly IConfiguration _configuration;
        private readonly Dictionary<string, DbConnectionDescriptor> _descriptors;

        /// <summary>
        /// 默认连接
        /// </summary>
        public DbConnectionDescriptor DefaultConnection { get; }

        /// <summary>
        /// 主机数据库上下文配置工厂
        /// </summary>
        public DbConnectionDescriptorFactory(
            IConfiguration configuration
            )
        {
            _configuration = configuration;
            _descriptors = new Dictionary<string, DbConnectionDescriptor>();
            this.Initialize();
            if (!_descriptors.Any()) throw new HostFriendlyException("ConnectionStrings config not found.");
            DefaultConnection = _descriptors[_descriptors.Keys.First()];
        }
        #endregion

        // 初始化
        private void Initialize()
        {
            // 添加数据库连接
            var connectionStrings = _configuration.GetSection("ConnectionStrings");//.GetSection("Configure").Get<string>();
            if (connectionStrings is null) throw new HostFriendlyException("ConnectionStrings config not found.");
            foreach (var section in connectionStrings.GetChildren())
            {
                var connectionName = section.Key;
                var connectionDefine = section.Get<string>();
                var info = new DbConnectionDescriptor(connectionName, connectionDefine);
                _descriptors[connectionName] = info;
            }
        }

        /// <summary>
        /// 获取连接描述
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public DbConnectionDescriptor GetConnection(string name)
        {
            if (_descriptors.ContainsKey(name)) throw new HostFriendlyException($"DbContextOptions '{name}' not found.");
            return _descriptors[name];
        }
    }
}
