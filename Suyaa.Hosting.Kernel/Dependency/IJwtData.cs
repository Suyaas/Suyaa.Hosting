namespace Suyaa.Hosting.Kernel.Dependency
{

    /// <summary>
    /// Jwt数据
    /// </summary>
    public interface IJwtData
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        long UserId { get; }
    }
}
