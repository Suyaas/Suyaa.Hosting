using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Suyaa.Data;
using Suyaa.Hosting.Configures;
using Suyaa.Hosting;
using Suyaa.Configure.Basic.Configures;
using Suyaa.Hosting.Dependency;
using Microsoft.EntityFrameworkCore;

namespace Suyaa.Configure.Host
{
    /// <summary>
    /// 启动器
    /// </summary>
    public class Startup : Hosting.StartupBase
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// 启动器
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfigure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //throw new NotImplementedException();
        }

        private DatabaseConnection GetConnection(string connectionString)
        {
            if (connectionString.IsNullOrWhiteSpace()) throw new HostException(I18n.Content("Configuration ConnectionStrings '{0}' not found.", "Configure"));
            if (connectionString[0] != '[') throw new HostException(I18n.Content("ConnectionString must start with '[dbtype]'."));
            int idx = connectionString.IndexOf(']');
            if (idx < 0) throw new HostException(I18n.Content("ConnectionString must start with '[dbtype]'."));
            string dbType = connectionString.Substring(1, idx - 1);
            string dbConnectionString = connectionString.Substring(idx + 1);
            switch (dbType)
            {
                case "Sqlite":
                    return new Suyaa.Data.DatabaseConnection(DatabaseTypes.Sqlite3, dbConnectionString);
                default:
                    throw new HostException(I18n.Content("Unsupported database type '{0}'.", dbType));
            }

        }

        protected override void OnConfigureServices(IServiceCollection services)
        {
            // 填充用户信息
            var users = _configuration.GetSection("Users");
            if (users is null) throw new HostException(I18n.Content("Configuration section '{0}' not found.", "Hosting"));
            var userConfigs = users.Get<UserConfigs>();
            services.AddSingleton(userConfigs);
            // 添加数据库连接
            var connectionString = _configuration.GetSection("ConnectionStrings").GetSection("Configure").Get<string>();
            services.AddScoped<IDatabaseConnection>(provider => GetConnection(connectionString));
            // 添加数据库上下文配置
            var optionsBuilder = new DbContextOptionsBuilder<DbContext>();
            optionsBuilder.UseSqlite(connectionString);
            var options = new HostDbContextOptions(optionsBuilder.Options, connectionString);
            services.AddSingleton(options);
        }

        protected override void OnInitialize()
        {
            this.Import<Apps.ModuleStartup>();
        }
    }
}