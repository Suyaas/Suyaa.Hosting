using Microsoft.AspNetCore.Mvc;

namespace Suyaa.Hosting.Dependency
{
    /// <summary>
    /// 相互映射特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class MapEachAttribute : Attribute
    {
        /// <summary>
        /// 目标类型
        /// </summary>
        public Type Type { get; }

        /// <summary>
        /// 相互映射特性
        /// </summary>
        /// <param name="type">目标类型</param>
        public MapEachAttribute(Type type)
        {
            Type = type;
        }
    }
}
