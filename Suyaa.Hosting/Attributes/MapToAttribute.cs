using Microsoft.AspNetCore.Mvc;
using Suyaa.Hosting.Attributes;

namespace Suyaa.Hosting.Attributes
{
    /// <summary>
    /// 映射目标特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class MapToAttribute : MapAttributeBase
    {
        /// <summary>
        /// 映射目标特性
        /// </summary>
        /// <param name="types">目标类型</param>
        public MapToAttribute(params Type[] types) : base(types) { }
    }
}
