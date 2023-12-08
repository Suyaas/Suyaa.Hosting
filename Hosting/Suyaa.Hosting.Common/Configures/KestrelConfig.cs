using Suyaa.Configure;
using Suyaa.Hosting.Common.Configures.Attributes;

namespace Suyaa.Hosting.Common.Configures
{
    /// <summary>
    /// 主机服务配置
    /// </summary>
    [Configuration("Kestrel")]
    public class KestrelConfig : IConfig
    {
        /// <summary>
        /// Endpoints
        /// </summary>
        public Dictionary<string, KestrelEndpoint> Endpoints { get; set; } = new Dictionary<string, KestrelEndpoint>();

        /// <summary>
        /// 生成默认配置
        /// </summary>
        public void Default()
        {
            this.Endpoints.Clear();
            this.Endpoints.Add("ep1", new KestrelEndpoint() { Url = "http://*:8088" });
        }
    }

    /// <summary>
    /// 主机服务Swagger配置
    /// </summary>
    public class KestrelEndpoint
    {

        /// <summary>
        /// 地址
        /// </summary>
        public string Url { get; set; } = string.Empty;

        /// <summary>
        /// 客户端证书类型
        /// </summary>
        public string? ClientCertificateMode { get; set; }

    }
}
