using Suyaa.Hosting.Common.Attributes;
using Suyaa.Hosting.Common.DependencyInjection.Dependency;
using Suyaa.Hosting.Common.Modules.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: Module("demo")]
namespace SimpleEfCoreHosting
{
    /// <summary>
    /// 模块启动
    /// </summary>
    public sealed class ModuleStartup : IModuleStartup
    {
        /// <summary>
        /// 依赖配置
        /// </summary>
        /// <param name="dependency"></param>
        public void ConfigureDependency(IDependencyManager dependency)
        {

        }
    }
}
