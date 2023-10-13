using Microsoft.EntityFrameworkCore;
using Suyaa.EFCore.SqlServer;
using Suyaa.Hosting.EFCore.Dependency;

namespace SqlServerDemo.Entities
{
    public class TestDbContext : SqlServerContextBase
    {
        private readonly IDbConnectionDescriptorFactory _dbConnectionDescriptorFactory;

        public TestDbContext(
            IDbConnectionDescriptorFactory dbConnectionDescriptorFactory
            ) : base(dbConnectionDescriptorFactory.DefaultConnection)
        {
            _dbConnectionDescriptorFactory = dbConnectionDescriptorFactory;
        }

        public DbSet<SystemObjects> SystemObjectses { get; set; }
    }
}
