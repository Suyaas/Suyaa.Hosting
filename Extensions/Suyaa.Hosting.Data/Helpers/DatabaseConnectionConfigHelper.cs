using Suyaa.Data.Dependency;
using Suyaa.Data;
using Suyaa.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Suyaa.Hosting.Data.Configures;

namespace Suyaa.Hosting.Data.Helpers
{
    /// <summary>
    /// 数据库连接助手
    /// </summary>
    public static class DatabaseConnectionConfigHelper
    {
        /// <summary>
        /// 获取数据库类型
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        /// <exception cref="DbProviderNotExistException"></exception>
        public static DatabaseType GetDatabaseType(this DatabaseConnectionConfig connection)
        {
            return connection.Database.ToLower() switch
            {
                "sqlite" => DatabaseType.Sqlite,
                "sqlite3" => DatabaseType.Sqlite3,
                "mssql" => DatabaseType.MicrosoftSqlServer,
                "sqlserver" => DatabaseType.MicrosoftSqlServer,
                "pqsql" => DatabaseType.PostgreSQL,
                "postgresql" => DatabaseType.PostgreSQL,
                "postgres" => DatabaseType.PostgreSQL,
                "mysql" => DatabaseType.MySQL,
                "access" => DatabaseType.MicrosoftOfficeAccess,
                "access12" => DatabaseType.MicrosoftOfficeAccessV12,
                _ => throw new DbProviderNotExistException(connection.Database),
            };
        }
    }
}
