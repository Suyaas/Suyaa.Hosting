using Suyaa.Hosting.Common.DependencyInjection.Dependency;
using Suyaa.Hosting.Common.Sessions.Dependency;

namespace Suyaa.Hosting.Sessions
{
    /// <summary>
    /// 内存交互信息
    /// </summary>
    public sealed class MemorySession : Dictionary<string, object>, ISession
    {
        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object? Get(string key)
        {
            if (!this.ContainsKey(key)) return null;
            return this[key];
        }

        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Set(string key, object? value)
        {
            // 空值则移除键
            if (value is null)
            {
                if (this.ContainsKey(key)) this.Remove(key);
                return;
            }
            // 设置值
            this[key] = value;
        }
    }
}
