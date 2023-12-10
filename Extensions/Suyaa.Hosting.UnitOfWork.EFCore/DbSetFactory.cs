using Microsoft.EntityFrameworkCore;
using Suyaa.EFCore.Dependency;
using Suyaa.EFCore.Descriptors;
using Suyaa.Hosting.Common.DependencyInjection.Dependency;
using Suyaa.Hosting.Common.DependencyInjection.Helpers;
using Suyaa.Hosting.UnitOfWork.EFCore.Dependency;
using Suyaa.Hosting.UnitOfWork.EFCore.Descriptors;
using System.Reflection;
using DbSetDescriptor = Suyaa.Hosting.UnitOfWork.EFCore.Descriptors.DbSetDescriptor;
using IDbSetFactory = Suyaa.Hosting.UnitOfWork.EFCore.Dependency.IDbSetFactory;
using IDbSetProvider = Suyaa.Hosting.UnitOfWork.EFCore.Dependency.IDbSetProvider;

namespace Suyaa.Hosting.UnitOfWork.EFCore
{
    /// <summary>
    /// 数据库上下文工厂
    /// </summary>
    public class DbSetFactory : IDbSetFactory
    {

        private readonly List<DbSetDescriptor> _dbSets;
        private readonly IDependencyManager _dependencyManager;
        private readonly IEnumerable<IDbSetProvider> _providers;

        /// <summary>
        /// 数据库上下文工厂
        /// </summary>
        public DbSetFactory(
            IDependencyManager dependencyManager
            )
        {
            _dbSets = new List<DbSetDescriptor>();
            _dependencyManager = dependencyManager;
            _providers = _dependencyManager.Resolves<IDbSetProvider>();
            // 添加所有 Dbset 描述
            foreach (var provider in _providers)
            {
                var dbSets = provider.GetDbSets();
                foreach (var dbSet in dbSets)
                {
                    if (_dbSets.Where(d => d.Type == dbSet.Type).Any()) continue;
                    _dbSets.Add(dbSet);
                }
            }
        }

        /// <summary>
        /// 获取实例
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public DbSetDescriptor? GetDbSet(Type type)
        {
            return _dbSets.Where(d => d.Type == type).FirstOrDefault();
        }

        /// <summary>
        /// 获取实例
        /// </summary>
        /// <returns></returns>
        public DbSetDescriptor? GetDbSet<TEntity>()
        {
            return GetDbSet(typeof(TEntity));
        }

        /// <summary>
        /// 获取连接中的所有数据实例
        /// </summary>
        /// <param name="dbConnectionDescriptorName"></param>
        /// <returns></returns>
        public IEnumerable<DbSetDescriptor> GetDbSets(string dbConnectionDescriptorName)
        {
            return _dbSets.Where(d => d.ConnectionDescriptor.Name == dbConnectionDescriptorName).ToList();
        }
    }
}
