namespace Suyaa.Hosting.EFCore
{
    ///// <summary>
    ///// 仓库供应商
    ///// </summary>
    //public class DbRepositoryProvider<TEntity, TId> : IDbQueryProvider<TEntity, TId>, IDbInsertProvider<TEntity, TId>, IDbUpdateProvider<TEntity, TId>, IDbDeleteProvider<TEntity, TId>
    //    where TEntity : class, IDbEntity<TId>
    //    where TId : notnull
    //{

    //    #region DI注入

    //    private readonly IDependencyManager _dependencyManager;
    //    private readonly IDbSetFactory _dbSetFactory;
    //    private readonly IDbContextWorkManager _dbContextWorkManager;

    //    /// <summary>
    //    /// 仓库供应商
    //    /// </summary>
    //    public DbRepositoryProvider(
    //        IDependencyManager dependencyManager,
    //        IDbSetFactory dbSetFactory,
    //        IDbContextWorkManager dbContextWorkManager
    //        )
    //    {
    //        _dependencyManager = dependencyManager;
    //        _dbSetFactory = dbSetFactory;
    //        _dbContextWorkManager = dbContextWorkManager;
    //    }
    //    #endregion

    //    // 获取实例描述
    //    private DbSetDescriptor GetDbEntity()
    //    {
    //        var descriptor = _dbSetFactory.GetDbSet<TEntity>();
    //        if (descriptor is null) throw new HostException($"Entity '{typeof(TEntity).FullName}' not found.");
    //        return descriptor;
    //    }

    //    // 获取实例描述
    //    private DescriptorDbContext GetDbContext(DbSetDescriptor descriptor)
    //    {
    //        var work = _dbContextWorkManager.GetWork();
    //        if (work is null) throw new HostException($"DbContextWork not found.");
    //        var dbContext = work.GetDbContext(descriptor.ConnectionDescriptor);
    //        if (dbContext is null)
    //        {
    //            //throw new HostException($"DbContext not found.");
    //            dbContext = (DescriptorDbContext)_dependencyManager.Resolve(descriptor.);
    //            work.SetDbContext(dbContext);
    //            dbContext = work.GetDbContext(descriptor.ConnectionDescriptor);
    //            if (dbContext is null) throw new HostException($"DbContext not found.");
    //        }
    //        return dbContext;
    //    }

    //    // 获取实例描述
    //    private IQueryable<TEntity> GetDbSet(DescriptorDbContext context, DbEntityModel entity)
    //    {
    //        //var dbSet = entity.DbSet.GetValue(context);
    //        //if (dbSet is null) throw new HostException($"DbSet '{entity.Entity.FullName}' not in context '{entity.Context.FullName}'.");
    //        //return (IQueryable<TEntity>)dbSet;
    //        return context.Set<TEntity>();
    //    }

    //    /// <summary>
    //    /// 获取查询
    //    /// </summary>
    //    /// <returns></returns>
    //    public IQueryable<TEntity> Query()
    //    {
    //        var descriptor = GetDbEntity();
    //        var context = GetDbContext(descriptor);
    //        return GetDbSet(context, descriptor);
    //    }

    //    /// <summary>
    //    /// 删除数据
    //    /// </summary>
    //    /// <param name="entity"></param>
    //    /// <returns></returns>
    //    public async Task DeleteAsync(TEntity entity)
    //    {
    //        var descriptor = GetDbEntity();
    //        var context = GetDbContext(descriptor);
    //        context.Set<TEntity>();
    //        context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
    //        await context.SaveChangesAsync();
    //    }

    //    /// <summary>
    //    /// 添加数据
    //    /// </summary>
    //    /// <param name="entity"></param>
    //    /// <returns></returns>
    //    public async Task InsertAsync(TEntity entity)
    //    {
    //        var descriptor = GetDbEntity();
    //        var context = GetDbContext(descriptor);
    //        context.Set<TEntity>();
    //        context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
    //        await context.SaveChangesAsync();
    //    }

    //    /// <summary>
    //    /// 更新数据
    //    /// </summary>
    //    /// <param name="entity"></param>
    //    /// <returns></returns>
    //    public async Task UpdateAsync(TEntity entity)
    //    {
    //        var descriptor = GetDbEntity();
    //        var context = GetDbContext(descriptor);
    //        context.Set<TEntity>();
    //        context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
    //        await context.SaveChangesAsync();
    //    }

    //    public void Delete(TEntity entity)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}