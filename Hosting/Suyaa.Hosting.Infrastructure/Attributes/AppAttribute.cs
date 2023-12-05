using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace Suyaa.Hosting.Kernel.Attributes
{
    /// <summary>
    /// 服务应用特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class AppAttribute : Attribute
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 服务应用特性
        /// </summary>
        /// <param name="name"></param>
        public AppAttribute(string name)
        {
            Name = name;
        }
    }
}
