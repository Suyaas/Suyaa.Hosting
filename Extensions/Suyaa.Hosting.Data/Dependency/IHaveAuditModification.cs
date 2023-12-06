namespace Suyaa.Hosting.Data.Dependency
{
    /// <summary>
    /// 拥有带审计的更新信息
    /// </summary>
    public interface IHaveAuditModification
    {
        /// <summary>
        /// 最后更新用户Id
        /// </summary>
        string LastModificationUserId { get; set; }
    }
}
