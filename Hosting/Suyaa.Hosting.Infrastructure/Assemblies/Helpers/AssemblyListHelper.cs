using System.Reflection;

namespace Suyaa.Hosting.Infrastructure.Assemblies.Helpers
{
    /// <summary>
    /// 字符串列表助手
    /// </summary>
    public static class AssemblyListHelper
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

        /// <summary>
        /// 从目录集合中查找并加载动态库文件
        /// </summary>
        /// <param name="assemblies"></param>
        /// <param name="paths"></param>
        /// <param name="library"></param>
        public static void ImportFromFile(this IList<Assembly> assemblies, IList<string> paths, string library)
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
    }
}
