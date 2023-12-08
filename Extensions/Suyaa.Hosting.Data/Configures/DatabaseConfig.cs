using Suyaa.Configure;
using Suyaa.Data;
using Suyaa.Data.Enums;
using Suyaa.Hosting.Common.Configures.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Hosting.Data.Configures
{
    /// <summary>
    /// 数据库配置
    /// </summary>
    [Configuration("Database")]
    public sealed class DatabaseConfig : IConfig
    {
        /// <summary>
        /// 连接集合
        /// </summary>
        public Dictionary<string, DatabaseConnectionConfig> Connections { get; set; } = new Dictionary<string, DatabaseConnectionConfig>();

        /// <summary>
        /// 默认数据库配置
        /// </summary>
        public void Default()
        {
            this.Connections.Add("defualt", new DatabaseConnectionConfig()
            {
                Database = "sqlite",
                ConnectionString = "Data Source=./data.db;"
            });
        }
    }

    /// <summary>
    /// 连接信息
    /// </summary>
    public sealed class DatabaseConnectionConfig
    {
        /// <summary>
        /// 数据库类型
        /// </summary>
        public string Database { get; set; } = string.Empty;

        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString { get; set; } = string.Empty;
    }
}
