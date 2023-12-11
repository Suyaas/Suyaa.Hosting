using Suyaa.Data.Dependency;
using Suyaa.Data.Factories;
using Suyaa.Hosting.Common.DependencyInjection.Dependency;
using Suyaa.Hosting.Common.DependencyInjection.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Hosting.Data.Factories
{
    /// <summary>
    /// 主机实体建模约定器工厂
    /// </summary>
    public sealed class HostEntityModelConventionFactory : EntityModelConventionFactory
    {
        /// <summary>
        /// 主机实体建模约定器工厂
        /// </summary>
        /// <param name="dependencyManager"></param>
        public HostEntityModelConventionFactory(IDependencyManager dependencyManager) : base(dependencyManager.Resolves<IEntityModelConvention>())
        {
        }
    }
}
