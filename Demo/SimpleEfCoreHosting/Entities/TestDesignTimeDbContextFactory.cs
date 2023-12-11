using Microsoft.Extensions.Configuration;
using Suyaa.Data;
using Suyaa.Data.Dependency;
using Suyaa.Data.Factories;
using Suyaa.Data.Providers;
using Suyaa.EFCore.Factories;
using Suyaa.Hosting.Data.Configures;
using Suyaa.Hosting.Common.Configures.Helpers;
using Suyaa.Hosting.Data.Factories;
using Suyaa.Hosting.Data.Providers;
using Suyaa.Hosting.Common.Configures;
using Suyaa;
using Suyaa.Hosting.Data.Helpers;
using Suyaa.Hosting.Common.DependencyInjection;
using Suyaa.Hosting.Common.DependencyInjection.Dependency;
using Suyaa.Hosting.Common.DependencyInjection.Helpers;

namespace SimpleEfCoreHosting.Entities
{
    public sealed class TestDesignTimeDbContextFactory : DesignTimeDbContextFactory<TestDbContext>
    {
        // 获取数据连接描述工厂
        private static IDbConnectionDescriptorFactory GetDbConnectionDescriptorFactory()
        {
            Console.WriteLine($"{nameof(GetDbConnectionDescriptorFactory)} start ...");
            var builder = new ConfigurationBuilder();
            var path = sy.Hosting.GetConfigurePath();
            Environment.CurrentDirectory = sy.Hosting.GetModulePath<TestDesignTimeDbContextFactory>();
            Console.WriteLine($"{nameof(GetDbConnectionDescriptorFactory)} path {path}");
            var source = builder.SetBasePath(path).AddConfigurationSource<DatabaseConfig>();
            builder.Build();
            var config = source.GetConfig();
            if (config is null) throw new NullException<DatabaseConfig>();
            //List<IDbConnectionDescriptor> descriptors = new List<IDbConnectionDescriptor>();
            //if (actionConfigurationBuilder != null) actionConfigurationBuilder(builder);
            //return builder.Build();
            DbConnectionDescriptorProvider dbConnectionDescriptorProvider = new DbConnectionDescriptorProvider();
            foreach (var connection in config.Connections)
            {
                var descriptor = new DbConnectionDescriptor(connection.Key, connection.Value.GetDatabaseType(), connection.Value.ConnectionString);
                dbConnectionDescriptorProvider.AddDbConnection(descriptor);
            }
            DbConnectionDescriptorFactory dbConnectionDescriptorFactory = new DbConnectionDescriptorFactory(new List<IDbConnectionDescriptorProvider> { dbConnectionDescriptorProvider });
            return dbConnectionDescriptorFactory;
        }

        // 获取实体建模约定器工厂
        private static IEntityModelConventionFactory GetEntityModelConventionFactory()
        {
            var dependencyManager = DependencyManager.GetCurrent();
            if (dependencyManager is null) throw new NullException<IDependencyManager>();
            return dependencyManager.ResolveRequired<IEntityModelConventionFactory>();
        }

        public TestDesignTimeDbContextFactory() : base(GetDbConnectionDescriptorFactory())
        {
            Console.WriteLine("TestDesignTimeDbContextFactory");
        }

        public override TestDbContext CreateDbContext(IDbConnectionDescriptorFactory dbConnectionDescriptorFactory, string[] args)
        {
            Console.WriteLine(dbConnectionDescriptorFactory.DefaultConnection.ToConnectionString());
            return new TestDbContext(new DbConnectionDescriptorManager(dbConnectionDescriptorFactory), GetEntityModelConventionFactory());
        }
    }
}
