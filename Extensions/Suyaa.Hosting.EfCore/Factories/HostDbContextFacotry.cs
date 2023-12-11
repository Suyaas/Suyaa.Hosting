using Suyaa.EFCore.Dependency;
using Suyaa.EFCore.Factories;
using Suyaa.Hosting.Common.DependencyInjection.Dependency;
using Suyaa.Hosting.Common.DependencyInjection.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Hosting.EfCore.Factories
{
    /// <summary>
    /// 主机数据库上下文工厂
    /// </summary>
    public sealed class HostDbContextFacotry : DbContextFacotry
    {
        /// <summary>
        /// 主机数据库上下文工厂
        /// </summary>
        /// <param name="dependencyManager"></param>
        public HostDbContextFacotry(IDependencyManager dependencyManager) : base(dependencyManager.Resolves<IDbContextProvider>())
        {
        }
    }
}
