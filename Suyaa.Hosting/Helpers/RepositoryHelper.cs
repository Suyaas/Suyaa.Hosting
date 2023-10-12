using Microsoft.Extensions.DependencyInjection;

namespace Suyaa.Hosting.Helpers
{
    /// <summary>
    /// 数据仓库相关接口
    /// </summary>
    public static class RepositoryHelper
    {
        ///// <summary>
        ///// 添加仓库相关接口注入
        ///// </summary>
        ///// <param name="services"></param>
        ///// <param name="action"></param>
        //public static void AddDbRepository(this IServiceCollection services, Action<DbContextOptionsBuilder> action)
        //{
        //    services.AddSingleton(typeof(DbContextOptionsBuilder), new DbContextOptionsBuilder<DbContext>());
        //    //services.AddSingleton<IRepositoryDbContexts, RepositoryDbContexts>();
        //}

        ///// <summary>
        ///// 添加仓库相关接口注入
        ///// </summary>
        ///// <param name="services"></param>
        //public static void AddDbRepositoryContext<T>(this IServiceCollection services) where T : DbContext
        //{
        //    //if (contexts is null)
        //    //{
        //    //    string msg = $"依赖注入'{typeof(T).FullName}'失败：请先使用'AddDbRepository'注入仓库依赖";
        //    //    egg.Logger.Debug(msg, "Macp");
        //    //    return;
        //    //}
        //    //contexts.Use<T>();
        //}
    }
}
