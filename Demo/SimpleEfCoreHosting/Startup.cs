using Microsoft.AspNetCore.Builder;
using Suyaa.Hosting;
using Suyaa.Hosting.Common.Configures.Helpers;
using Suyaa.Hosting.Common.DependencyInjection.Dependency;
using Suyaa.Hosting.Infrastructure.Assemblies.Helpers;
using Suyaa.Hosting.Multilingual.Helpers;
using Suyaa.Hosting.UnitOfWork.Helpers;
using Suyaa.Hosting.WebApplications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Suyaa.Hosting.UnitOfWork.EFCore.Helpers;
using Suyaa.Hosting.Jwt.Helpers;

namespace SimpleEfCoreHosting
{
    /// <summary>
    /// 启动器
    /// </summary>
    public sealed class Startup : HostStartup
    {
        protected override void OnConfigureBuilder(WebApplicationBuilder builder)
        {
            builder.AddConfigures();
            base.OnConfigureBuilder(builder);
            builder.AddDatabaseConfigure();
            builder.AddJwtConfigure();
        }
        protected override void OnConfigureDependency(IDependencyManager dependencyManager)
        {
            base.OnConfigureDependency(dependencyManager);
            dependencyManager.AddEfCoreUnitOfWork();
            dependencyManager.AddJwt();
        }
        protected override void OnConfigureAssembly(IList<Assembly> assemblies)
        {
            base.OnConfigureAssembly(assemblies);
            assemblies.Import<ModuleStartup>();
        }
    }
}
