using Microsoft.EntityFrameworkCore;
using Suyaa.EFCore.Dependency;
using Suyaa.EFCore.Descriptors;
using Suyaa.Hosting.Common.DependencyInjection.Dependency;
using Suyaa.Hosting.Common.DependencyInjection.Helpers;
using Suyaa.Hosting.UnitOfWork.EFCore.Dependency;
using Suyaa.Hosting.UnitOfWork.EFCore.Descriptors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DbSetDescriptor = Suyaa.Hosting.UnitOfWork.EFCore.Descriptors.DbSetDescriptor;
using IDbSetProvider = Suyaa.Hosting.UnitOfWork.EFCore.Dependency.IDbSetProvider;

namespace Suyaa.EFCore.Providers
{
    /// <summary>
    /// DbSet 供应商
    /// </summary>
    public class DbSetProvider : IDbSetProvider
    {
        private readonly IDependencyManager _dependencyManager;
        private readonly IEnumerable<IDescriptorDbContext> _dbContexts;
        private static Type _dbSetType = typeof(DbSet<>);

        /// <summary>
        /// DbSet 供应商
        /// </summary>
        public DbSetProvider(
            IDependencyManager dependencyManager
            )
        {
            _dependencyManager = dependencyManager;
            _dbContexts = _dependencyManager.Resolves<IDescriptorDbContext>();

        }

        /// <summary>
        /// 获取 DbSet 集合
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DbSetDescriptor> GetDbSets()
        {
            List<DbSetDescriptor> descriptors = new List<DbSetDescriptor>();
            foreach (var dbContext in _dbContexts)
            {
                var dbSets = GetDbContextDbSets(dbContext);
                descriptors.AddRange(dbSets);
            }
            return descriptors;
        }

        // 添加数据库实例
        private List<DbSetDescriptor> GetDbContextDbSets(IDescriptorDbContext dbContext)
        {
            List<DbSetDescriptor> descriptors = new List<DbSetDescriptor>();
            var type = dbContext.GetType();
            var pros = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var prop in pros)
            {
                // 跳过非泛型
                if (!prop.PropertyType.IsGenericType) continue;
                // 获取泛型实现
                var genericType = prop.PropertyType.GetGenericTypeDefinition();
                if (_dbSetType.IsAssignableFrom(genericType))
                {
                    descriptors.Add(new DbSetDescriptor(type, prop, dbContext));
                }
            }
            return descriptors;
        }
    }
}
