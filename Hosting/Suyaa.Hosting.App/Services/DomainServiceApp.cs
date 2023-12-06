using Microsoft.AspNetCore.Mvc;
using Suyaa.Hosting.App.Dependency;
using Suyaa.Hosting.Common.Services;

namespace Suyaa.Hosting.App.Services
{
    /// <summary>
    /// 服务应用
    /// </summary>
    public abstract class DomainServiceApp : ServiceBase, IDomainServiceApp
    {
    }
}
