using Microsoft.EntityFrameworkCore;
using Suyaa.DependencyInjection;
using Suyaa.EFCore.SqlServer;
using Suyaa.Hosting.EFCore.Dependency;
using Suyaa.Hosting.EFCores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
