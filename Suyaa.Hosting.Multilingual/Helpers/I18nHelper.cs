﻿using Suyaa.Hosting.Configures;
using Suyaa.Hosting.Dependency;
using Suyaa.Hosting.Kernel;

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
        public static HostFriendlyException FriendlyException(this IMultilingualManager multilingualManager, string message, params string[] args)
        {
            return new HostFriendlyException(multilingualManager.Content(message, args));
        }

        /// <summary>
        /// 触发一个错误
        /// </summary>
        /// <param name="i18n"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static HostException Exception(this IMultilingualManager multilingualManager, string message, params string[] args)
        {
            return new HostException(multilingualManager.Content(message, args));
        }
    }
}