namespace Suyaa.Hosting.Kernel.Dependency
{
    /// <summary>
    /// 交互信息
    /// </summary>
    public interface ISession
    {
        /// <summary>
        /// 用户唯一标识
        /// </summary>
        string? Uid { get; set; }

        /// <summary>
        /// 租户Id
        /// </summary>
        int? TenantId { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        DateTime? InvalidTime { get; set; }
    }
}
