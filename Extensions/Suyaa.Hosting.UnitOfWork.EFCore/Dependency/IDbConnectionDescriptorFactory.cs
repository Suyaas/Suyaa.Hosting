using Suyaa.Data;

namespace Suyaa.Hosting.EFCore.Dependency
{
    /// <summary>
    /// 主机数据库上下文配置工厂
    /// </summary>
    public interface IDbConnectionDescriptorFactory
    {
        /// <summary>
        /// 获取主机数据库上下文配置
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        DbConnectionDescriptor GetConnection(string name);

        /// <summary>
        /// 默认主机数据库上下文配置
        /// </summary>
        DbConnectionDescriptor DefaultConnection { get; }
    }
}
