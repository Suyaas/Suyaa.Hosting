namespace Suyaa.Hosting.Data.Dependency
{
    /// <summary>
    /// 拥有带审计的创建信息
    /// </summary>
    public interface IHaveAuditCreation : IHaveCreation
    {
        /// <summary>
        /// 创建用户Id
        /// </summary>
        string CreationUserId { get; set; }
    }
}
