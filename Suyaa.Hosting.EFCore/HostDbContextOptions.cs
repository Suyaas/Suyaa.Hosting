using Microsoft.EntityFrameworkCore;
using Suyaa.Hosting.Data;

namespace Suyaa.Hosting.EFCores
{
    /// <summary>
    /// 主机上下文选项
    /// </summary>
    public class HostDbContextOptions
    {

        /// <summary>
        /// 连接字符串
        /// </summary>
        public DbConnectionDescriptor ConnectionInfo { get; }

        /// <summary>
        /// 连接名称
        /// </summary>
        public string ConnectionName { get; }

        /// <summary>
        /// 主机上下文选项
        /// </summary>
        /// <param name="options"></param>
        /// <param name="connectionString"></param>
        public HostDbContextOptions(string connectionName, DbConnectionDescriptor info)
        {
            ConnectionInfo = info;
            ConnectionName = connectionName;
        }

    }
}
