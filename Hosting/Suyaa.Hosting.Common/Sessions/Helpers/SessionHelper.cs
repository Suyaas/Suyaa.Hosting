using Suyaa.Hosting.Common.Sessions.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Hosting.Common.Sessions.Helpers
{
    /// <summary>
    /// 交互信息助手
    /// </summary>
    public static class SessionHelper
    {
        /// <summary>
        /// 获取值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T? Get<T>(this ISession session, string key)
        {
            return (T?)session.Get(key);
        }
    }
}
