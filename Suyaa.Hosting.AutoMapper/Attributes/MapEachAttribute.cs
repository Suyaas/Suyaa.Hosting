using Microsoft.AspNetCore.Mvc;

namespace Suyaa.Hosting.AutoMapper.Attributes
{
    /// <summary>
    /// 相互映射特性
    /// </summary>
    public class MapEachAttribute : MapAttributeBase
    {
        /// <summary>
        /// 相互映射特性
        /// </summary>
        /// <param name="types">目标类型</param>
        public MapEachAttribute(params Type[] types) : base(types) { }
    }
}
