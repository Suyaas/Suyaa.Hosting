using Suyaa.Configure;

namespace Suyaa.Hosting.Dependency
{
    /// <summary>
    /// 主机配置
    /// </summary>
    public interface IOptionConfig<T> where T : IConfig
    {
        /// <summary>
        /// 当前值
        /// </summary>
        T CurrentValue { get; }
    }
}
