using Suyaa.Configure;

namespace Suyaa.Hosting.Multilingual.Configures
{
    /// <summary>
    /// 多语言配置信息
    /// </summary>
    public class I18nConfig : IConfig
    {
        /// <summary>
        /// 语言名称
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 语言描述
        /// </summary>
        public string Decription { get; set; } = string.Empty;

        /// <summary>
        /// 语句配置
        /// </summary>
        public List<I18nStatement> Statements { get; set; } = new List<I18nStatement>();

        /// <summary>
        /// 默认值
        /// </summary>
        public void Default()
        {
            Name = "zh_cn";
            Decription = "Simplified Chinese";
            I18nStatement statement = new I18nStatement()
            {
                Content = "Notice: {0}",
                Decription = "Notice: {0}",
            };
            statement.Key = Decription.GetSha256();
            Statements.Add(statement);
        }
    }

    /// <summary>
    /// 多语言语句
    /// </summary>
    public class I18nStatement
    {
        /// <summary>
        /// 语句唯一键
        /// </summary>
        public string Key { get; set; } = string.Empty;
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; } = string.Empty;
        /// <summary>
        /// 描述
        /// </summary>
        public string Decription { get; set; } = string.Empty;
    }
}
