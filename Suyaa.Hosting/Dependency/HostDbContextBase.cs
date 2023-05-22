using Microsoft.EntityFrameworkCore;
using Suyaa.EFCore;

namespace Suyaa.Hosting.Dependency
{
    /// <summary>
    /// 主机数据库上下文基类
    /// </summary>
    public abstract class HostDbContextBase : DbContextBase
    {
        /// <summary>
        /// 主机数据库上下文基类
        /// </summary>
        /// <param name="options"></param>
        protected HostDbContextBase(HostDbContextOptions options) : base(options.Options, options.ConnectionString)
        {
        }
    }
}
