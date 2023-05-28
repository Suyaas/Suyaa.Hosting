﻿using Suyaa.Configure;
using Suyaa;
using Suyaa.Hosting.Configures;

namespace Suyaa.Hosting.Dependency
{
    /// <summary>
    /// 多语言支持
    /// </summary>
    public class I18n : II18n
    {
        // 存储路径
        private readonly JsonConfigManager<I18nConfig> _configManager;

        /// <summary>
        /// 多语言支持
        /// </summary>
        /// <param name="configManager">配置器</param>
        public I18n(JsonConfigManager<I18nConfig> configManager)
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