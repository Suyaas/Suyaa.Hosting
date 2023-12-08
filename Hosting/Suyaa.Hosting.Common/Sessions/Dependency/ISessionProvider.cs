namespace Suyaa.Hosting.Common.Sessions.Dependency
{
    /// <summary>
    /// 交互信息
    /// </summary>
    public interface ISessionProvider
    {
        /// <summary>
        /// 创建一个
        /// </summary>
        /// <returns></returns>
        ISession GetSession();
    }
}
