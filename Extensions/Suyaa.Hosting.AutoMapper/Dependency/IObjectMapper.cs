namespace Suyaa.Hosting.AutoMapper.Dependency
{
    /// <summary>
    /// 对象映射器
    /// </summary>
    public interface IObjectMapper
    {
        /// <summary>
        /// 映射对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public T Map<T>(object data);
    }
}
