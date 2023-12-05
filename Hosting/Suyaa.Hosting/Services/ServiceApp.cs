using Microsoft.AspNetCore.Mvc;
using Suyaa.Hosting.App.Dependency;

namespace Suyaa.Hosting.Services
{
    /// <summary>
    /// 服务应用
    /// </summary>
    public abstract class ServiceApp : ServiceBase, IDomainServiceApp
    {
    }
}
