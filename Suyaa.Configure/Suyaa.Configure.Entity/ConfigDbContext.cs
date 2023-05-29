using Microsoft.EntityFrameworkCore;
using Suyaa.Configure.Entity.Projects;
using Suyaa.EFCore;
using Suyaa.EFCore.Helpers;
using Suyaa.Hosting.Dependency.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// <param name="options"></param>
        public ConfigDbContext(HostDbContextOptions options) : base(options)
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
