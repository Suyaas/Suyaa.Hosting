using Suyaa.Data;
using Suyaa.Data.Dependency;
using Suyaa.Data.Descriptors;
using Suyaa.Data.Helpers;
using Suyaa.Data.SimpleDbWorks;
using Suyaa.Hosting.Common.DependencyInjection.Dependency;
using Suyaa.Hosting.Common.DependencyInjection.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Hosting.Data.Factories
{
    /// <summary>
    /// 数据库工厂
    /// </summary>
    public sealed class DbFactory : IDbFactory
    {
        private readonly List<EntityDescriptor> _entities;
        private readonly IDbConnectionDescriptor _dbConnectionDescriptor;
        private readonly IEnumerable<IDbEntityProvider> _dbEntityProviders;
        private static object _lock = new object();
        private long _indexer;

        #region 依赖注入

        private readonly IDependencyManager _dependencyManager;
        private readonly IDbConnectionDescriptorFactory _dbConnectionDescriptorFactory;

        /// <summary>
        /// 数据库工厂
        /// </summary>
        public DbFactory(
            IDependencyManager dependencyManager,
            IDbConnectionDescriptorFactory dbConnectionDescriptorFactory
            )
        {
            _dependencyManager = dependencyManager;
            _dbConnectionDescriptorFactory = dbConnectionDescriptorFactory;
            _entities = new List<EntityDescriptor>();
            _dbConnectionDescriptor = _dbConnectionDescriptorFactory.DefaultConnection;
            _dbEntityProviders = _dependencyManager.Resolves<IDbEntityProvider>();
            this.WorkProvider = _dependencyManager.ResolveRequired<IDbWorkProvider>();
            this.Provider = _dbConnectionDescriptor.DatabaseType.GetDbProvider();
            this.WorkManagerProvider = _dependencyManager.ResolveRequired<IDbWorkManagerProvider>();
            _indexer = 0;
        }

        #endregion

        /// <summary>
        /// 实例集合
        /// </summary>
        public IList<EntityDescriptor> Entities => _entities;

        /// <summary>
        /// 作业供应商
        /// </summary>
        public IDbWorkProvider WorkProvider { get; }

        /// <summary>
        /// 作业管理器供应商
        /// </summary>
        public IDbWorkManagerProvider WorkManagerProvider { get; }

        /// <summary>
        /// 数据库供应商
        /// </summary>
        public IDbProvider Provider { get; }

        /// <summary>
        /// 添加实例
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <exception cref="KeyException"></exception>
        public EntityDescriptor AddEntity(Type type)
        {
            // 判断是否实现了接口
            if (!type.HasInterface<IEntity>()) throw new TypeNotSupportedException(type);
            // 判断是否已经存在
            if (_entities.Where(d => d.Type == type).Any()) throw new ExistException(type.FullName ?? string.Empty);
            // 建立实例描述
            EntityDescriptor entity = new EntityDescriptor(type);
            // 触发供应商事件
            foreach (var provider in _dbEntityProviders)
            {
                provider.OnEntityModeling(entity);
            }
            // 建立字段描述
            var pros = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var prop in pros)
            {
                long idx = GetParamterIndex();
                var field = new FieldDescriptor(idx, prop);
                // 触发供应商事件
                foreach (var provider in _dbEntityProviders)
                {
                    provider.OnFieldModeling(entity, field);
                }
                entity.Fields.Add(field);
            }
            _entities.Add(entity);
            return entity;
        }

        public EntityDescriptor GetEntity(Type type)
        {
            var entiy = _entities.Where(d => d.Type == type).FirstOrDefault();
            if (entiy is null) entiy = AddEntity(type);
            return entiy;
        }

        /// <summary>
        /// 获取新的参数索引
        /// </summary>
        /// <returns></returns>
        public long GetParamterIndex()
        {
            long index = 0;
            lock (_lock)
            {
                index = ++_indexer;
            }
            return index;
        }
    }
}
