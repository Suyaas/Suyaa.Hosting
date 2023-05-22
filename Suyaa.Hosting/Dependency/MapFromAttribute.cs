using Microsoft.AspNetCore.Mvc;

namespace Suyaa.Hosting.Dependency
{
    /// <summary>
    /// 映射源特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class MapFromAttribute : Attribute
    {
        /// <summary>
        /// 源类型
        /// </summary>
        public Type Type { get; }

        /// <summary>
        /// 映射源特性
        /// </summary>
        /// <param name="type">源类型</param>
        public MapFromAttribute(Type type)
        {
            Type = type;
        }
    }
}
