using Microsoft.AspNetCore.Mvc;

namespace Suyaa.Hosting.AutoMapper.Attributes
{
    /// <summary>
    /// 相互映射特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public abstract class MapAttributeBase : Attribute
    {
        /// <summary>
        /// 目标类型
        /// </summary>
        public List<Type> Types { get; }

        /// <summary>
        /// 相互映射特性
        /// </summary>
        /// <param name="types">目标类型</param>
        public MapAttributeBase(params Type[] types)
        {
            Types = types.ToList();
        }
    }
}
