using Suyaa.Data;
using Suyaa.EFCore.SqlServer;
using Suyaa.Hosting.EFCore.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Configure.Basic.EFCore
{
    /// <summary>
    /// 主机数据库上下文
    /// </summary>
    public class HostDbContextBase : SqlServerContextBase
    {
        /// <summary>
        /// 主机数据库上下文
        /// </summary>
        /// <param name="dbConnectionDescriptorFactory"></param>
        public HostDbContextBase(IDbConnectionDescriptorFactory dbConnectionDescriptorFactory) : base(dbConnectionDescriptorFactory.DefaultConnection)
        {
        }
    }
}
