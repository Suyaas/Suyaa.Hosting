using SqlServerDemo.Entities;
using Suyaa.Hosting.Kernel.Dependency;
using Suyaa.Hosting.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlServerDemo.Apps
{
    /// <summary>
    /// SqlServer
    /// </summary>
    public class SqlServerServiceApp : ServiceApp
    {
        private readonly IRepository<SystemObjects, long> _systemObjectsRepository;

        public SqlServerServiceApp(
            IRepository<SystemObjects, long> systemObjectsRepository
            )
        {
            _systemObjectsRepository = systemObjectsRepository;
        }

        /// <summary>
        /// 测试
        /// </summary>
        /// <returns></returns>
        public async Task Test() { await Task.CompletedTask; }
    }
}
