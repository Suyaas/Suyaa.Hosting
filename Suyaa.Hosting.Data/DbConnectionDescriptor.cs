using Suyaa.Data;
using Suyaa.Hosting.Kernel;

namespace Suyaa.Hosting.Data
{
    /// <summary>
    /// 连接信息
    /// </summary>
    public class DbConnectionDescriptor
    {
        /// <summary>
        /// 数据库类型
        /// </summary>
        public DatabaseTypes DatabaseType { get; }
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString { get; }

        /// <summary>
        /// 连接信息
        /// </summary>
        /// <param name="defineString"></param>
        /// <exception cref="HostException"></exception>
        public DbConnectionDescriptor(string defineString)
        {
            if (defineString.IsNullOrWhiteSpace()) throw new HostException(string.Format("Configuration ConnectionStrings '{0}' not found.", "Configure"));
            if (defineString[0] != '[') throw new HostException(string.Format("ConnectionString must start with '[dbtype]'."));
            int idx = defineString.IndexOf(']');
            if (idx < 0) throw new HostException(string.Format("ConnectionString must start with '[dbtype]'."));
            string dbType = defineString.Substring(1, idx - 1);
            // 获取连接字符串
            this.ConnectionString = defineString.Substring(idx + 1);
            // 获取数据库类型
            this.DatabaseType = dbType.ToLower() switch
            {
                "sqlite" => DatabaseTypes.Sqlite,
                "sqlite3" => DatabaseTypes.Sqlite3,
                "mssql" or "sqlserver" => DatabaseTypes.MicrosoftSqlServer,
                "pqsql" or "postgresql" or "postgres" => DatabaseTypes.PostgreSQL,
                "mysql" => DatabaseTypes.MySQL,
                "access" => DatabaseTypes.MicrosoftOfficeAccess,
                "access12" => DatabaseTypes.MicrosoftOfficeAccessV12,
                _ => throw new HostException(string.Format("Unsupported database type '{0}'.", dbType)),
            };
        }
    }
}