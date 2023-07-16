namespace Suyaa.Hosting.Dependency
{
    /// <summary>
    /// Jwt数据管理器
    /// </summary>
    public interface IJwtDataManager : IJwtData
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        IJwtData? Data { get; set; }
    }

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

    /// <summary>
    /// Jwt数据类型
    /// </summary>
    public interface IJwtDataType
    {
        /// <summary>
        /// 数据类型
        /// </summary>
        Type Type { get; set; }
    }
}
