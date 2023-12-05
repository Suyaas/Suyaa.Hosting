using Suyaa.Hosting.Configures;
using System.Reflection;

namespace Suyaa.Hosting.Kernel.Helpers
{
    /// <summary>
    /// 主机配置助手
    /// </summary>
    public static partial class ListHelper
    {
        /// <summary>
        /// 导入程序集
        /// </summary>
        /// <param name="assemblies"></param>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static IList<Assembly> Import(this IList<Assembly> assemblies, Assembly? assembly)
        {
            if (assembly is null) return assemblies;
            if (!assemblies.Contains(assembly)) assemblies.Add(assembly);
            return assemblies;
        }

        /// <summary>
        /// 导入程序集
        /// </summary>
        /// <param name="assemblies"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static IList<Assembly> Import(this IList<Assembly> assemblies, string path)
        {
            return assemblies.Import(Assembly.LoadFrom(path));
        }

        /// <summary>
        /// 导入程序集
        /// </summary>
        /// <param name="assemblies"></param>
        /// <param name="tp"></param>
        /// <returns></returns>
        public static IList<Assembly> Import(this IList<Assembly> assemblies, Type? tp)
        {
            return assemblies.Import(tp?.Assembly);
        }

        /// <summary>
        /// 导入程序集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IList<Assembly> Import<T>(this IList<Assembly> assemblies) where T : class
        {
            return assemblies.Import(typeof(T).Assembly);
        }

        // 加载库文件
        private static void ImportLibrary(IList<Assembly> assemblies, IList<string> paths, string library)
        {
            // 所有所有路径
            for (int i = 0; i < paths.Count; i++)
            {
                string filePath = sy.IO.CombinePath(paths[i], library);
                if (sy.IO.FileExists(filePath))
                {
                    assemblies.Import(filePath);
                    continue;
                }
            }
        }

        /// <summary>
        /// 获取主机配置
        /// </summary>
        /// <param name="assemblies"></param>
        /// <param name="hostConfig"></param>
        /// <param name="paths"></param>
        /// <returns></returns>
        public static IList<Assembly> AddFromHostConfig(this IList<Assembly> assemblies, HostConfig hostConfig, IList<string> paths)
        {
            // 加载所有的程序集
            for (int i = 0; i < hostConfig.Libraries.Count; i++)
            {
                ImportLibrary(assemblies, paths, hostConfig.Libraries[i]);
            }
            return assemblies;
        }
    }
}
