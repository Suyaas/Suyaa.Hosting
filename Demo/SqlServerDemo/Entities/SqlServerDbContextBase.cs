using Microsoft.EntityFrameworkCore;
using SqlServerDemo.Helpers;
using Suyaa.Hosting.Data;
using Suyaa.Hosting.EFCores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlServerDemo.Entities
{
    /// <summary>
    /// SqlServer数据库上下文
    /// </summary>
    public abstract class SqlServerDbContextBase : HostDbContextBase
    {
        /// <summary>
        /// SqlServer数据库上下文
        /// </summary>
        /// <param name="descriptor"></param>
        protected SqlServerDbContextBase(DbConnectionDescriptor descriptor) : base(descriptor.GetSqlServerContextOptions())
        {
        }
    }
}
