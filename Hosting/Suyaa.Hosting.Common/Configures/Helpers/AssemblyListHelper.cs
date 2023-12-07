using Suyaa.Hosting.Infrastructure.Assemblies.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Hosting.Common.Configures.Helpers
{
    /// <summary>
    /// 程序集列表助手
    /// </summary>
    public static class AssemblyListHelper
    {
        /// <summary>
        /// 从主机配置中导入程序集
        /// </summary>
        /// <param name="assemblies"></param>
        /// <param name="hostConfig"></param>
        /// <param name="paths"></param>
        /// <returns></returns>
        public static IList<Assembly> ImportFromHostConfig(this IList<Assembly> assemblies, HostConfig hostConfig, IList<string> paths)
        {
            // 加载所有的程序集
            for (int i = 0; i < hostConfig.Libraries.Count; i++)
            {
                assemblies.ImportFromFile(paths, hostConfig.Libraries[i]);
            }
            return assemblies;
        }
    }
}
