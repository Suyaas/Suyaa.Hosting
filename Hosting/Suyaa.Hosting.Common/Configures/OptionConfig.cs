using Suyaa.Configure;
using Suyaa.Hosting.Common.Configures.Dependency;
using Suyaa.Hosting.Common.DependencyInjection.Dependency;

namespace Suyaa.Hosting.Common.Configures
{
    /// <summary>
    /// 可选配置
    /// </summary>
    public class OptionConfig
    {
        private readonly object _value;

        /// <summary>
        /// 可选配置
        /// </summary>
        /// <param name="dependencyManager"></param>
        /// <param name="type"></param>
        public OptionConfig(IDependencyManager dependencyManager, Type type)
        {
            var obj = dependencyManager.Resolve(type);
            if (obj is null)
            {
                obj = sy.Assembly.Create(type) ?? throw new TypeNotSupportedException(type);
                if (obj is IConfig config) config.Default();
            }
            _value = obj;
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <returns></returns>
        public object GetValue()
        {
            return _value;
        }
    }
    /// <summary>
    /// 可选配置
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class OptionConfig<T> : OptionConfig, IOptionConfig<T> where T : IConfig, new()
    {

        /// <summary>
        /// 配置值
        /// </summary>
        public T CurrentValue => (T)GetValue();

        /// <summary>
        /// 主机配置
        /// </summary>
        /// <param name="dependencyManager"></param>
        public OptionConfig(IDependencyManager dependencyManager) : base(dependencyManager, typeof(T))
        {
        }
    }
}
