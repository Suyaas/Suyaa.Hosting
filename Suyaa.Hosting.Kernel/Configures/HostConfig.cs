using Suyaa.Configure;

namespace Suyaa.Hosting.Configures
{
    /// <summary>
    /// 主机服务配置
    /// </summary>
    public class HostConfig : IConfig
    {
        /// <summary>
        /// 寻址路径集合
        /// </summary>
        public List<string> Paths { get; set; }

        /// <summary>
        /// 程序集设置
        /// </summary>
        public List<string> Libraries { get; set; }

        /// <summary>
        /// 日志路径
        /// </summary>
        public string LogPath { get; set; }

        /// <summary>
        /// 是否记录详细日志
        /// </summary>
        public bool IsVerboseLog { get; set; }

        /// <summary>
        /// 是否支持跨域
        /// </summary>
        public bool IsCorsAll { get; set; }

        /// <summary>
        /// 是否显示Swagger
        /// </summary>
        public bool IsSwagger { get; set; }

        /// <summary>
        /// Swagger配置
        /// </summary>
        public List<HostSwaggerConfig> Swaggers { get; set; }

        /// <summary>
        /// 舒雅服务配置
        /// </summary>
        public HostConfig()
        {
            // 设置默认值
            this.Paths = new List<string>();
            this.Libraries = new List<string>();
            this.LogPath = string.Empty;
            this.IsCorsAll = false;
            this.Swaggers = new List<HostSwaggerConfig>();
        }

        /// <summary>
        /// 获取默认配置
        /// </summary>
        public void Default()
        {
            // 设置默认日志路径
            this.LogPath = "~/logs";
            // 添加默认路径
            this.Paths.Add("~/libs");
            // 添加默认的swagger配置
            this.Swaggers.Add(new HostSwaggerConfig()
            {
                Name = "all",
                Description = "All APIs",
                Keyword = "*"
            });
        }
    }

    /// <summary>
    /// 主机服务Swagger配置
    /// </summary>
    public class HostSwaggerConfig
    {

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// 过滤关键字
        /// </summary>
        public string Keyword { get; set; } = string.Empty;

    }
}
