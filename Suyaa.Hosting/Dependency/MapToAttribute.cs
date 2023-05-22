using Microsoft.AspNetCore.Mvc;

namespace Suyaa.Hosting.Dependency
{
    /// <summary>
    /// 映射目标特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class MapToAttribute : Attribute
    {
        /// <summary>
        /// 目标类型
        /// </summary>
        public Type Type { get; }

        /// <summary>
        /// 映射目标特性
        /// </summary>
        /// <param name="type">目标类型</param>
        public MapToAttribute(Type type)
        {
            Type = type;
        }
    }
}
