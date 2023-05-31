using Microsoft.EntityFrameworkCore;

namespace Suyaa.Hosting.EFCores
{
    /// <summary>
    /// 主机上下文选项
    /// </summary>
    public class HostDbContextOptions
    {
        /// <summary>
        /// 数据库设置
        /// </summary>
        public DbContextOptions Options { get; }

        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString { get; }

        /// <summary>
        /// 主机上下文选项
        /// </summary>
        /// <param name="options"></param>
        /// <param name="connectionString"></param>
        public HostDbContextOptions(DbContextOptions options, string connectionString)
        {
            Options = options;
            ConnectionString = connectionString;
        }


    }
}
