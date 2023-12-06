using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace Suyaa.Hosting.Common.Attributes
{
    /// <summary>
    /// 模块特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly)]
    public class ModuleAttribute : Attribute
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 模块特性
        /// </summary>
        /// <param name="name"></param>
        public ModuleAttribute(string name)
        {
            Name = name;
        }
    }
}
