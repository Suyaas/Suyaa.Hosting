using Suyaa.DependencyInjection;
using Suyaa.EFCore;
using Suyaa.Hosting.Data.Dependency;
using Suyaa.Hosting.EFCore.Dependency;
using Suyaa.Hosting.Kernel;
using Suyaa.Hosting.Kernel.Dependency;
using System.Collections.Generic;

namespace Suyaa.Hosting.EFCore
{
    /// <summary>
    /// 仓库供应商
    /// </summary>
    public class DbRepositoryProvider<TEntity, TId> : IQueryProvider<TEntity, TId>, IInsertProvider<TEntity, TId>, IUpdateProvider<TEntity, TId>, IDeleteProvider<TEntity, TId>
        where TEntity : class, IEntity<TId>
        where TId : notnull
    {

        #region DI注入

        private readonly IDependencyManager _dependencyManager;
        private readonly IDbContextFactory _dbContextFactory;

        /// <summary>
        /// 仓库供应商
        /// </summary>
        public DbRepositoryProvider(
            IDependencyManager dependencyManager,
            IDbContextFactory dbContextFactory
            )
        {
            _dependencyManager = dependencyManager;
            _dbContextFactory = dbContextFactory;
        }
        #endregion

        // 获取实例描述
        private DbEntityDescriptor GetDbEntity()
        {
            var descriptor = _dbContextFactory.GetEntity<TEntity>();
            if (descriptor is null) throw new HostException($"Entity '{typeof(TEntity).FullName}' not found.");
            return descriptor;
        }

        // 获取实例描述
        private DbContextBase GetDbContext(DbEntityDescriptor descriptor)
        {
            return (DbContextBase)_dependencyManager.Resolve(descriptor.Context);
        }

        // 获取实例描述
        private IQueryable<TEntity> GetDbSet(DbContextBase context, DbEntityDescriptor entity)
        {
            var dbSet = entity.DbSet.GetValue(context);
            if (dbSet is null) throw new HostException($"DbSet '{entity.Entity.FullName}' not in context '{entity.Context.FullName}'.");
            return (IQueryable<TEntity>)dbSet;
        }

        /// <summary>
        /// 获取查询
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public IQueryable<TEntity> Query()
        {
            var descriptor = GetDbEntity();
            var context = GetDbContext(descriptor);
            return GetDbSet(context, descriptor);
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task DeleteAsync(TEntity entity)
        {
            var descriptor = GetDbEntity();
            var context = GetDbContext(descriptor);
            context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task InsertAsync(TEntity entity)
        {
            var descriptor = GetDbEntity();
            var context = GetDbContext(descriptor);
            context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateAsync(TEntity entity)
        {
            var descriptor = GetDbEntity();
            var context = GetDbContext(descriptor);
            context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await context.SaveChangesAsync();
        }
    }
}