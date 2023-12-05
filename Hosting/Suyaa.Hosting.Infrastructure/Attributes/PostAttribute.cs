using Microsoft.AspNetCore.Mvc;

namespace Suyaa.Hosting.Kernel.Attributes
{
    /// <summary>
    /// Post方法特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class PostAttribute : HttpPostAttribute
    {
        /// <summary>
        /// Post方法特性
        /// </summary>
        public PostAttribute() : base("[action]") { }

        /// <summary>
        /// Post方法特性
        /// </summary>
        public PostAttribute(string name) : base(name) { }
    }
}
