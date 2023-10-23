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
    public class DbRepositoryProvider<TEntity, TId> : IDbQueryProvider<TEntity, TId>, IInsertProvider<TEntity, TId>, IDbUpdateProvider<TEntity, TId>, IDbDeleteProvider<TEntity, TId>
        where TEntity : class, IEntity<TId>
        where TId : notnull
    {

        #region DI注入

        private readonly IDependencyManager _dependencyManager;
        private readonly IDbContextFactory _dbContextFactory;
        private readonly IDbContextAsyncProvider _dbContextAsyncProvider;

        /// <summary>
        /// 仓库供应商
        /// </summary>
        public DbRepositoryProvider(
            IDependencyManager dependencyManager,
            IDbContextFactory dbContextFactory,
            IDbContextAsyncProvider dbContextAsyncProvider
            )
        {
            _dependencyManager = dependencyManager;
            _dbContextFactory = dbContextFactory;
            _dbContextAsyncProvider = dbContextAsyncProvider;
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
        private DbDescriptorContext GetDbContext(DbEntityDescriptor descriptor)
        {
            var work = _dbContextAsyncProvider.GetCurrentWork();
            if (work is null) throw new HostException($"DbContextWork not found.");
            var dbContext = work.GetDbContext(descriptor.ConnectionDescriptor);
            if (dbContext is null)
            {
                //throw new HostException($"DbContext not found.");
                dbContext = (DbDescriptorContext)_dependencyManager.Resolve(descriptor.Context);
                work.SetDbContext(dbContext);
                dbContext = work.GetDbContext(descriptor.ConnectionDescriptor);
                if (dbContext is null) throw new HostException($"DbContext not found.");
            }
            return dbContext;
        }

        // 获取实例描述
        private IQueryable<TEntity> GetDbSet(DbDescriptorContext context, DbEntityDescriptor entity)
        {
            //var dbSet = entity.DbSet.GetValue(context);
            //if (dbSet is null) throw new HostException($"DbSet '{entity.Entity.FullName}' not in context '{entity.Context.FullName}'.");
            //return (IQueryable<TEntity>)dbSet;
            return context.Set<TEntity>();
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
            context.Set<TEntity>();
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
            context.Set<TEntity>();
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
            context.Set<TEntity>();
            context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await context.SaveChangesAsync();
        }
    }
}