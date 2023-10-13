using Microsoft.EntityFrameworkCore;
using Suyaa.DependencyInjection;
using Suyaa.EFCore.Dependency;
using Suyaa.Hosting.EFCore.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Hosting.EFCore
{
    /// <summary>
    /// 数据库上下文工厂
    /// </summary>
    public class DbContextFactory : IDbContextFactory
    {

        #region DI注入

        private readonly IDependencyManager _dependencyManager;
        private readonly List<DbEntityDescriptor> _dbEntities;
        private readonly Type _dbSetType;

        /// <summary>
        /// 数据库上下文工厂
        /// </summary>
        public DbContextFactory(
            IDependencyManager dependencyManager
            )
        {
            _dependencyManager = dependencyManager;
            _dbEntities = new List<DbEntityDescriptor>();
            _dbSetType = typeof(DbSet<>);
            // 添加数据库上下文
            this.AddDbContexts();
        }

        #endregion

        // 添加数据库实例
        private void AddDbContextEntities(Type type)
        {
            var pros = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var prop in pros)
            {
                // 跳过非泛型
                if (!prop.PropertyType.IsGenericType) continue;
                // 获取泛型实现
                var genericType = prop.PropertyType.GetGenericTypeDefinition();
                if (_dbSetType.IsAssignableFrom(genericType))
                {
                    _dbEntities.Add(new DbEntityDescriptor(type, prop));
                }
            }
        }

        // 添加数据库上下文
        private void AddDbContexts()
        {
            var types = _dependencyManager.GetResolveTypes<IDbContext>();
            foreach (var type in types) this.AddDbContextEntities(type);
        }

        /// <summary>
        /// 获取实例
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public DbEntityDescriptor? GetEntity(Type type)
        {
            return _dbEntities.Where(d => d.Entity == type).FirstOrDefault();
        }

        /// <summary>
        /// 获取实例
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public DbEntityDescriptor? GetEntity<TEntity>()
        {
            return GetEntity(typeof(TEntity));
        }
    }
}
