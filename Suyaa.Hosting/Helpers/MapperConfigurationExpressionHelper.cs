using AutoMapper;
using System.Reflection;

namespace Suyaa.Hosting.Helpers
{
    /// <summary>
    /// AutoMapper配置助手
    /// </summary>
    public static class MapperConfigurationExpressionHelper
    {
        /// <summary>
        /// 创建映射配置集合
        /// </summary>
        /// <param name="cfg"></param>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static IMapperConfigurationExpression CreateMaps(this IMapperConfigurationExpression cfg, Assembly assembly)
        {
            return cfg;
        }

        /// <summary>
        /// 创建映射配置集合
        /// </summary>
        /// <param name="cfg"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static IMapperConfigurationExpression CreateMaps(this IMapperConfigurationExpression cfg, List<Assembly> assemblies)
        {
            foreach (Assembly assembly in assemblies) cfg.CreateMaps(assembly);
            return cfg;
        }
    }
}
