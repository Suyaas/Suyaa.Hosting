namespace Suyaa.Microservice.Configures
{
    /// <summary>
    /// 舒雅服务配置
    /// </summary>
    public class SuyaaSetting
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
        /// 舒雅服务配置
        /// </summary>
        public SuyaaSetting()
        {
            // 设置默认值
            this.Paths = new List<string>();
            this.Libraries = new List<string>();
            this.LogPath = "~/logs";
        }

    }
}
