using Suyaa.Configure;
using Suyaa.Hosting.Common.Configures.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Hosting.Multilingual.Configures
{
    /// <summary>
    /// 多语言配置
    /// </summary>
    [Configuration("multilingual")]
    public class MultilingualConfig : IConfig
    {
        /// <summary>
        /// 路径
        /// </summary>
        public string Path { get; set; } = string.Empty;
        /// <summary>
        /// 语言
        /// </summary>
        public string Language { get; set; } = string.Empty;

        /// <summary>
        /// 默认配置
        /// </summary>
        public void Default()
        {
            this.Path = "./i18n";
            this.Language = "zh_cn";
        }
    }
}
