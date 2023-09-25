using Suyaa.Configure;
using Suyaa;
using System.Diagnostics;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Suyaa.Hosting.Kernel;

namespace Suyaa.Hosting.Helpers
{
    /// <summary>
    /// 配置结点助手
    /// </summary>
    public static class ConfigurationSectionHelper
    {
        /// <summary>
        /// 转化为配置项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="section"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static T To<T>(this IConfigurationSection section, Func<T>? func = null) where T : IConfig, new()
        {
            // 处理配置结点为空
            if (section is null)
            {
                if (func is null) throw new HostException("Configuration section not found.");
                return func();
            }
            T cfg = new T();
            var type = typeof(T);
            var pros = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var sections = section.GetChildren();
            foreach (var pro in pros)
            {
                var value = section.GetSection(pro.Name).Get(pro.PropertyType);
                if (value != null) pro.SetValue(cfg, value);
            }
            return cfg;
        }
    }
}
