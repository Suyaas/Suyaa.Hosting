using Microsoft.AspNetCore.Mvc;

namespace Suyaa.Hosting.Dependency
{
    /// <summary>
    /// 服务应用特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class AppAttribute : RouteAttribute
    {
        /// <summary>
        /// 服务应用特性
        /// </summary>
        /// <param name="route"></param>
        public AppAttribute(string route) : base($"app/{route}") { }
    }
}
