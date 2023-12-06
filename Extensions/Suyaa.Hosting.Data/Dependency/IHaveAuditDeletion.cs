namespace Suyaa.Hosting.Kernel.Dependency
{
    /// <summary>
    /// 拥有带审计的删除信息
    /// </summary>
    public interface IHaveAuditDeletion : IHaveDeletion
    {
        /// <summary>
        /// 删除用户Id
        /// </summary>
        string DeletionUserId { get; set; }
    }
}
