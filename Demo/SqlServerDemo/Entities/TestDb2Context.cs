using Microsoft.EntityFrameworkCore;
using Suyaa.EFCore.SqlServer;
using Suyaa.Hosting.EFCore.Dependency;

namespace SqlServerDemo.Entities
{
    public class TestDb2Context : SqlServerContext
    {
        private readonly IDbConnectionDescriptorFactory _dbConnectionDescriptorFactory;

        public TestDb2Context(
            IDbConnectionDescriptorFactory dbConnectionDescriptorFactory
            ) : base(dbConnectionDescriptorFactory.DefaultConnection)
        {
            _dbConnectionDescriptorFactory = dbConnectionDescriptorFactory;
            //this.Set<SystemObjects>();
            //this.Set<SystemTables>();
        }

        public DbSet<SystemObjects> SystemObjectses { get; set; }
    }
}
