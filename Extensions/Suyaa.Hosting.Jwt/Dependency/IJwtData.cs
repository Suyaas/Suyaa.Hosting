namespace Suyaa.Hosting.Jwt.Dependency
{

    /// <summary>
    /// Jwt数据
    /// </summary>
    public interface IJwtData
    {
        /// <summary>
        /// 用户唯一标识
        /// </summary>
        string Uid { get; }

        /// <summary>
        /// 租户Id
        /// </summary>
        int TenantId { get; }
    }
}
