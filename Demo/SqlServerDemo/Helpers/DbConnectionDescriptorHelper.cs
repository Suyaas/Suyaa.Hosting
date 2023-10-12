using Microsoft.EntityFrameworkCore;
using Suyaa.Data;
using Suyaa.Hosting.Data;
using Suyaa.Hosting.Kernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlServerDemo.Helpers
{
    /// <summary>
    /// 主机数据库上下文配置助手
    /// </summary>
    public static class DbConnectionDescriptorHelper
    {
        /// <summary>
        /// 获取SqlServer
        /// </summary>
        /// <param name="descriptor"></param>
        /// <returns></returns>
        public static DbContextOptions GetSqlServerContextOptions(this DbConnectionDescriptor descriptor)
        {
            if (descriptor.DatabaseType != DatabaseTypes.MicrosoftSqlServer) throw new HostException($"DatabaseType '{descriptor.DatabaseType}' not supported.");
            // 添加数据库上下文配置
            var optionsBuilder = new DbContextOptionsBuilder<DbContext>();
            optionsBuilder.UseSqlServer(descriptor.ConnectionString);
            return optionsBuilder.Options;
        }
    }
}
