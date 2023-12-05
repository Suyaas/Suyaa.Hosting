namespace Suyaa.Hosting.AutoMapper.Attributes
{
    /// <summary>
    /// 映射源特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class MapFromAttribute : MapAttributeBase
    {
        /// <summary>
        /// 映射源特性
        /// </summary>
        /// <param name="types">源类型</param>
        public MapFromAttribute(params Type[] types) : base(types) { }
    }
}
