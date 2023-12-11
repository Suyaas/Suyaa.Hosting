using Microsoft.EntityFrameworkCore;
using Suyaa.Data.Dependency;
using Suyaa.EFCore.Contexts;
using Suyaa.EFCore.Helpers;

namespace SimpleEfCoreHosting.Entities
{
    public class TestDbContext : DescriptorDbContext
    {
        public DbSet<Test> Tests { get; set; }

        public TestDbContext(IDbConnectionDescriptorManager dbConnectionDescriptorManager) : base(dbConnectionDescriptorManager.GetCurrentConnection(), dbConnectionDescriptorManager.GetCurrentConnection().DatabaseType.GetEfCoreProvider().DbContextOptionsProvider.GetDbContextOptions(dbConnectionDescriptorManager.GetCurrentConnection().ToConnectionString()))
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<People>().HasIndex(p => p.Name);
        }
    }
}
