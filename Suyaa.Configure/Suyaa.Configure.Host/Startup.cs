using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Suyaa.Data;
using Suyaa.Configure.Basic.Configures;
using Suyaa.DependencyInjection;
using Suyaa.Hosting.Kernel;
using Suyaa.Hosting;
using Suyaa.Hosting.Multilingual.Helpers;
using Suyaa.Hosting.Jwt.Helpers;
using Suyaa.Configure.Basic.Jwt;

namespace Suyaa.Configure.Host
{
    /// <summary>
    /// 启动器
    /// </summary>
    public class Startup : HostStartupBase
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
            if (connectionString.IsNullOrWhiteSpace()) throw new HostException(string.Format("Configuration ConnectionStrings '{0}' not found.", "Configure"));
            if (connectionString[0] != '[') throw new HostException(string.Format("ConnectionString must start with '[dbtype]'."));
            int idx = connectionString.IndexOf(']');
            if (idx < 0) throw new HostException(string.Format("ConnectionString must start with '[dbtype]'."));
            string dbType = connectionString.Substring(1, idx - 1);
            string dbConnectionString = connectionString.Substring(idx + 1);
            switch (dbType)
            {
                case "Sqlite":
                    return new Suyaa.Data.DatabaseConnection(DbTypes.Sqlite3, dbConnectionString);
                default:
                    throw new HostException(string.Format("Unsupported database type '{0}'.", dbType));
            }

        }

        //// 获取连接配置
        //private HostDbContextOptions GetDbContextOptions(string connectionString)
        //{
        //    if (connectionString.IsNullOrWhiteSpace()) throw new HostException(string.Format("Configuration ConnectionStrings '{0}' not found.", "Configure"));
        //    if (connectionString[0] != '[') throw new HostException(string.Format("ConnectionString must start with '[dbtype]'."));
        //    int idx = connectionString.IndexOf(']');
        //    if (idx < 0) throw new HostException(string.Format("ConnectionString must start with '[dbtype]'."));
        //    string dbType = connectionString.Substring(1, idx - 1);
        //    string dbConnectionString = connectionString.Substring(idx + 1);
        //    // 添加数据库上下文配置
        //    var optionsBuilder = new DbContextOptionsBuilder<DbContext>();
        //    switch (dbType)
        //    {
        //        case "Sqlite":
        //            optionsBuilder.UseSqlite(dbConnectionString);
        //            break;
        //        default:
        //            throw new HostException(string.Format("Unsupported database type '{0}'.", dbType));
        //    }
        //    var options = new HostDbContextOptions("Configure", dbConnectionString, optionsBuilder.Options);
        //    return options;
        //}

        /// <summary>
        /// 依赖配置
        /// </summary>
        /// <param name="dependency"></param>
        /// <exception cref="HostException"></exception>
        protected override void OnConfigureDependency(IDependencyManager dependency)
        {
            // 添加Jwt支持
            dependency.AddJwt<JwtDataProvider>();
            // 添加EFCore支持
            dependency.AddEFCore();
            // 使用多语言支持
            //dependency.AddMultilingual();
            // 使用工作单元
            //dependency.AddUnitOfWork();
            // 填充用户信息
            var users = _configuration.GetSection("Users");
            if (users is null) throw new HostException(string.Format("Configuration section '{0}' not found.", "Hosting"));
            var userConfigs = users.Get<UserConfigs>();
            dependency.Register(userConfigs);
            //// 添加数据库连接
            //var connectionString = _configuration.GetSection("ConnectionStrings").GetSection("Configure").Get<string>();
            //services.AddScoped<IDatabaseConnection>(provider => GetConnection(connectionString));
            // 添加数据库上下文
            //services.AddSingleton(GetDbContextOptions(connectionString));
            // 注册应用授权信息和
            //services.AddScoped<IAppInfo, AppInfo>();
            //services.AddScoped<IJwtData, JwtInfo>();
            // 设置支持的Jwt类型
            //services.AddScoped<IJwtDataType>(provider => new JwtDataType(typeof(JwtInfo)));
        }

        protected override void OnInitialize()
        {
            this.Import<Apps.ModuleStartup>();
        }

        /// <summary>
        /// 创建依赖管理器
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        protected override IDependencyManager OnDependencyManagerCreating(IServiceCollection services)
        {
            return new DependencyInjection.ServiceCollection.DependencyManager(services);
        }
    }
}