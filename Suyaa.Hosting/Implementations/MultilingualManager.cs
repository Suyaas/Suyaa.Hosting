using Suyaa.Configure;
using Suyaa.DependencyInjection;
using Suyaa.Hosting.Dependency;

namespace Suyaa.Hosting.Configures
{
    /// <summary>
    /// 多语言支持
    /// </summary>
    public class MultilingualManager : IMultilingualManager, IDependencyTransient
    {

        /// <summary>
        /// 多语言支持
        /// </summary>
        public MultilingualManager()
        {
        }

        /// <summary>
        /// 获取内容
        /// </summary>
        /// <param name="content"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public string Content(string content, params object?[] args)
        {
            return string.Format(content, args);
        }
    }
}
