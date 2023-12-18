using Suyaa.Configure;
using Suyaa.Hosting.Common.Configures.Attributes;

namespace Suyaa.Hosting.Redirects.Configures
{
    /// <summary>
    /// 跳转配置
    /// </summary>
    [Configuration("Redirect")]
    public class RedirectConfig : IConfig
    {
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enable { get; set; } = false;

        /// <summary>
        /// 配置集合
        /// </summary>
        public List<RedirectConfigItem> Redirects { get; set; } = new List<RedirectConfigItem>();

        /// <summary>
        /// 获取默认配置
        /// </summary>
        public void Default()
        {
            Enable = false;
            Redirects.Add(new RedirectConfigItem() { Source = "/", Dest = "/inex.html" });
        }
    }

    /// <summary>
    /// 跳转配置
    /// </summary>
    public sealed class RedirectConfigItem
    {

        /// <summary>
        /// 源地址
        /// </summary>
        public string Source { get; set; } = string.Empty;

        /// <summary>
        /// 新地址
        /// </summary>
        public string Dest { get; set; } = string.Empty;

    }
}
