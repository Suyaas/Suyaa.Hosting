using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Suyaa.Hosting.Common.Configures;
using Suyaa.Hosting.Common.DependencyManager.Dependency;
using Suyaa.Hosting.Common.WebApplications;
using Suyaa.Hosting.Infrastructure.WebApplications;
using Suyaa.Hosting.Infrastructure.WebApplications.Dependency;
using Suyaa.Hosting.Kernel;
using System.Diagnostics;

namespace Suyaa.Hosting.Core.WebApplications
{
    /// <summary>
    /// Web应用核心启动器
    /// </summary>
    public class CoreStartup : CommonStartup
    {

    }
}
