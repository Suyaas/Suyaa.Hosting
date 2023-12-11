using Microsoft.EntityFrameworkCore;
using Suyaa.Data.Dependency;
using Suyaa.EFCore.Contexts;
using Suyaa.EFCore.Helpers;
using Suyaa.Hosting.EfCore.DbContexts;

namespace SimpleEfCoreHosting.Entities
{
    public class TestDbContext : HostDbContext
    {
        public DbSet<Test> Tests { get; set; }

        public TestDbContext(IDbConnectionDescriptorManager dbConnectionDescriptorManager, IEntityModelConventionFactory entityModelConventionFactory)
            : base(dbConnectionDescriptorManager, entityModelConventionFactory)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<People>().HasIndex(p => p.Name);
        }
    }
}
