using Suyaa.Configure;
using Suyaa.Hosting.Common.DependencyInjection.Dependency;
using Suyaa.Hosting.Multilingual.Configures;
using Suyaa.Hosting.Multilingual.Dependency;

namespace Suyaa.Hosting.Configures
{
    /// <summary>
    /// 多语言支持
    /// </summary>
    public class MultilingualManager : IMultilingualManager, IDependencyTransient, IDependencyExclusive
    {
        // 存储路径
        private readonly JsonConfigManager<I18nConfig> _configManager;

        /// <summary>
        /// 多语言支持
        /// </summary>
        /// <param name="configManager">配置器</param>
        public MultilingualManager(
            JsonConfigManager<I18nConfig> configManager)
        {
            _configManager = configManager;
        }

        /// <summary>
        /// 获取内容
        /// </summary>
        /// <param name="content"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public string Content(string content, params object?[] args)
        {
            string key = content.GetSha256();
            var statement = _configManager.Config.Statements.Where(d => d.Key == key).FirstOrDefault();
            if (statement is null)
            {
                statement = new I18nStatement()
                {
                    Content = content,
                    Decription = content,
                    Key = key,
                };
                _configManager.Config.Statements.Add(statement);
                _configManager.Save();
            }
            return string.Format(statement.Content, args);
        }
    }
}
