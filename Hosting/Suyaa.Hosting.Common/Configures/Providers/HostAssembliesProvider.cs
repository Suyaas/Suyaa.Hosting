using Suyaa.Hosting.Infrastructure.Assemblies.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Hosting.Common.Configures.Providers
{
    /// <summary>
    /// 主机程序集聚合供应商
    /// </summary>
    public class HostAssembliesProvider : IAssembliesProvider
    {
        private readonly HostConfig _hostConfig;

        /// <summary>
        /// 主机程序集聚合供应商
        /// </summary>
        /// <param name="hostConfig"></param>
        public HostAssembliesProvider(HostConfig hostConfig)
        {
            _hostConfig = hostConfig;
        }

        /// <summary>
        /// 获取程序集聚合
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Assembly> GetAssemblies()
        {
            throw new NotImplementedException();
        }
    }
}
