using Suyaa.Configure;

namespace Suyaa.Microservice.Configures
{
    /// <summary>
    /// 舒雅服务Swagger配置
    /// </summary>
    public class SuyaaConfigSwagger
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
