using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Suyaa.Data;

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

        protected override void OnConfigureServices(IServiceCollection services)
        {
            // 添加数据库连接
            var connectionString = _configuration.GetSection("ConnectionStrings").GetSection("Hosting").Get<string>();
            //IDatabaseConnection connection = new Suyaa.Data.DatabaseConnection(DatabaseTypes.Sqlite3, connectionString);
            services.AddScoped<IDatabaseConnection>(provider => new Suyaa.Data.DatabaseConnection(DatabaseTypes.Sqlite3, connectionString));
        }

        protected override void OnInitialize()
        {
            //throw new NotImplementedException();
        }
    }
}