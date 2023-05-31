using Microsoft.AspNetCore.Mvc;

namespace Suyaa.Hosting.Attributes
{
    /// <summary>
    /// Put方法特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class PutAttribute : HttpPutAttribute
    {
        /// <summary>
        /// Put方法特性
        /// </summary>
        public PutAttribute() : base("[action]") { }
        /// <summary>
        /// Put方法特性
        /// </summary>
        public PutAttribute(string name) : base(name) { }
    }
}
