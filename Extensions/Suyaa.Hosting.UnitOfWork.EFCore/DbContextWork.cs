namespace Suyaa.Hosting.EFCore
{
    ///// <summary>
    ///// 数据库上下文异步作业
    ///// </summary>
    //public class DbContextWork : Disposable, IDbContextWork
    //{
    //    private readonly IDependencyManager _dependencyManager;
    //    private readonly IDbSetFactory _dbSetFactory;

    //    #region DI注入

    //    private readonly IDbContextWorkManager _dbContextWorkManager;
    //    private readonly Dictionary<string, DescriptorTypeDbContext> _dbContexts;
    //    //private DynamicDbContext? _dbContext;

    //    /// <summary>
    //    /// 数据库上下文异步作业
    //    /// </summary>
    //    public DbContextWork(
    //        IDependencyManager dependencyManager,
    //        IDbSetFactory dbSetFactory,
    //        IDbContextWorkManager dbContextWorkManager
    //        )
    //    {
    //        this.IsCompleted = false;
    //        _dependencyManager = dependencyManager;
    //        _dbSetFactory = dbSetFactory;
    //        _dbContextWorkManager = dbContextWorkManager;
    //        _dbContexts = new Dictionary<string, DescriptorTypeDbContext>();
    //    }

    //    #endregion

    //    /// <summary>
    //    /// 获取数据库上下文
    //    /// </summary>
    //    /// <param name="descriptor"></param>
    //    /// <returns></returns>
    //    public DescriptorDbContext? GetDbContext(DbConnectionDescriptor descriptor)
    //    {
    //        if (_dbContexts.ContainsKey(descriptor.Name)) return _dbContexts[descriptor.Name];
    //        return null;
    //    }

    //    /// <summary>
    //    /// 设置数据库上下文
    //    /// </summary>
    //    /// <param name="dbContext"></param>
    //    public void SetDbContext(DescriptorDbContext dbContext)
    //    {
    //        if (_dbContexts.ContainsKey(dbContext.ConnectionDescriptor.Name)) return;
    //        _dbContexts.Add(dbContext.ConnectionDescriptor.Name, new DescriptorTypeDbContext(dbContext.ConnectionDescriptor, dbContext.Options, _dbContextFactory.GetEntityTypes(dbContext.ConnectionDescriptor.Name)));
    //    }

    //    /// <summary>
    //    /// 是否完成
    //    /// </summary>
    //    public bool IsCompleted { get; private set; }

    //    /// <summary>
    //    /// 完成
    //    /// </summary>
    //    /// <returns></returns>
    //    public async Task CompleteAsync()
    //    {
    //        // 设置已完成
    //        this.IsCompleted = true;
    //        // 清除
    //        _dbContextAsyncProvider.SetCurrentWork(null);
    //        await Task.CompletedTask;
    //    }

    //    /// <summary>
    //    /// 释放资源
    //    /// </summary>
    //    protected override void OnManagedDispose()
    //    {
    //        // 未完成，则自动清理作业
    //        if (!this.IsCompleted)
    //        {
    //            // 清除
    //            _dbContextAsyncProvider.SetCurrentWork(null);
    //        }
    //        // 清除变量
    //        _dbContexts.Clear();
    //        base.OnManagedDispose();
    //    }
    //}
}
