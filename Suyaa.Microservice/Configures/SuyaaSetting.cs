using Suyaa.Configure;

namespace Suyaa.Microservice.Configures
{
    /// <summary>
    /// 舒雅服务配置
    /// </summary>
    public class SuyaaSetting : JsonSetting
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
        public List<SuyaaSettingSwagger> Swaggers { get; set; }

        /// <summary>
        /// 舒雅服务配置
        /// </summary>
        public SuyaaSetting()
        {
            // 设置默认值
            this.Paths = new List<string>();
            this.Libraries = new List<string>();
            this.LogPath = "~/logs";
            this.IsCorsAll = false;
            this.Swaggers = new List<SuyaaSettingSwagger>();
        }

    }
}
