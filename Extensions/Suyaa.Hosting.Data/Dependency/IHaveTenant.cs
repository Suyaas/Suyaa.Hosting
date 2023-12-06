namespace Suyaa.Hosting.Data.Dependency
{
    /// <summary>
    /// 拥有租户
    /// </summary>
    public interface IHaveTenant
    {
        /// <summary>
        /// 租户Id
        /// </summary>
        int TenantId { get; set; }
    }
}
