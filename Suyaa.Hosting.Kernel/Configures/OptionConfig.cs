using Suyaa.Configure;
using Suyaa.Hosting.Kernel.Dependency;

namespace Suyaa.Hosting.Kernel.Configures
{
    /// <summary>
    /// 可选配置
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class OptionConfig<T> : IOptionConfig<T> where T : IConfig, new()
    {
        /// <summary>
        /// 配置值
        /// </summary>
        public T CurrentValue { get; }

        /// <summary>
        /// 主机配置
        /// </summary>
        /// <param name="value"></param>
        public OptionConfig(T? value)
        {
            if (value is null)
            {
                CurrentValue = new T();
                CurrentValue.Default();
            }
            else
            {
                CurrentValue = value;
            }
        }
    }
}
