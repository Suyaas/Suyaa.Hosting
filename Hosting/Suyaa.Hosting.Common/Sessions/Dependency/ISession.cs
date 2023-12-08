namespace Suyaa.Hosting.Common.Sessions.Dependency
{
    /// <summary>
    /// 交互信息
    /// </summary>
    public interface ISession
    {
        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void Set(string key, object? value);

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        object? Get(string key);
    }
}
