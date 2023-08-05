using Suyaa.Hosting.Configures;

namespace Suyaa.Hosting.Helpers
{
    /// <summary>
    /// 多语言助手
    /// </summary>
    public static class I18nHelper
    {
        /// <summary>
        /// 触发一个友好错误
        /// </summary>
        /// <param name="i18n"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static HostFriendlyException FriendlyException(this I18n i18n, string message, params string[] args)
        {
            return new HostFriendlyException(i18n.Content(message, args));
        }

        /// <summary>
        /// 触发一个错误
        /// </summary>
        /// <param name="i18n"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static HostException Exception(this I18n i18n, string message, params string[] args)
        {
            return new HostException(i18n.Content(message, args));
        }
    }
}
