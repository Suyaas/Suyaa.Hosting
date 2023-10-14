using Microsoft.EntityFrameworkCore;
using Suyaa.Configure.Basic.EFCore;
using Suyaa.Configure.Entity.Projects;
using Suyaa.EFCore.Helpers;
using Suyaa.Hosting.EFCore.Dependency;

namespace Suyaa.Configure.Entities
{
    /// <summary>
    /// 数据库连接上下文
    /// </summary>
    public class ConfigDbContext : HostDbContextBase
    {

        /// <summary>
        /// 项目
        /// </summary>
        public DbSet<Project> Projects { get; set; }

#nullable disable

        /// <summary>
        /// 数据库连接上下文
        /// </summary>
        /// <param name="dbConnectionDescriptorFactory"></param>
        public ConfigDbContext(IDbConnectionDescriptorFactory dbConnectionDescriptorFactory) : base(dbConnectionDescriptorFactory)
        {
        }

#nullable enable

        /// <summary>
        /// 模型创建
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.BuildToLowerName<ConfigDbContext>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
