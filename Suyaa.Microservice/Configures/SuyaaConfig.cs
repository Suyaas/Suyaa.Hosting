using Suyaa.Configure;

namespace Suyaa.Microservice.Configures
{
    /// <summary>
    /// 舒雅服务配置
    /// </summary>
    public class SuyaaConfig : IConfig
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
        public List<SuyaaConfigSwagger> Swaggers { get; set; }

        /// <summary>
        /// 舒雅服务配置
        /// </summary>
        public SuyaaConfig()
        {
            // 设置默认值
            this.Paths = new List<string>();
            this.Libraries = new List<string>();
            this.LogPath = string.Empty;
            this.IsCorsAll = false;
            this.Swaggers = new List<SuyaaConfigSwagger>();
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
            this.Swaggers.Add(new SuyaaConfigSwagger()
            {
                Name = "all",
                Description = "All APIs",
                Keyword = "*"
            });
        }
    }
}
