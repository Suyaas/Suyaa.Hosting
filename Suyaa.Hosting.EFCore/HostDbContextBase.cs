using Microsoft.EntityFrameworkCore;
using Suyaa.DependencyInjection;
using Suyaa.EFCore;
using Suyaa.EFCore.Helpers;
using Suyaa.Hosting.EFCore.Dependency;

namespace Suyaa.Hosting.EFCores
{
    /// <summary>
    /// 主机数据库上下文基类
    /// </summary>
    public abstract class HostDbContextBase : DbContext, IHostDbContext
    {
        /// <summary>
        /// 主机数据库上下文基类
        /// </summary>
        /// <param name="options"></param>
        protected HostDbContextBase(DbContextOptions options) : base(options)
        {

        }
    }
}
