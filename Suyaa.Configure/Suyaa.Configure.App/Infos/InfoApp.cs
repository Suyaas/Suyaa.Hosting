using Microsoft.AspNetCore.Mvc;
using Suyaa.Hosting.Services;

namespace Suyaa.Configure.Apps.Infos
{
    /// <summary>
    /// 信息
    /// </summary>
    public sealed class InfoApp : ServiceApp
    {
        /// <summary>
        /// 获取版本号
        /// </summary>
        /// <returns></returns>
        public string GetVersion()
        {
            return sy.Assembly.Version;
        }
    }
}
