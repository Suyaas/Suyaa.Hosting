using Suyaa.Data.Dependency;
using Suyaa.EFCore.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Hosting.EfCore.DbContexts
{
    /// <summary>
    /// 主机数据库上下文
    /// </summary>
    public abstract class HostDbContext : DescriptorDbContext
    {
        /// <summary>
        /// 主机数据库上下文
        /// </summary>
        /// <param name="dbConnectionDescriptorManager"></param>
        /// <param name="entityModelConventionFactory"></param>
        protected HostDbContext(IDbConnectionDescriptorManager dbConnectionDescriptorManager, IEntityModelConventionFactory entityModelConventionFactory)
            : base(dbConnectionDescriptorManager.GetCurrentConnection(), entityModelConventionFactory)
        {
        }
    }
}
