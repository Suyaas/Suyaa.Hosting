using AutoMapper;
using Suyaa.Hosting.AutoMapper.Dependency;
using Suyaa.Hosting.Common.DependencyInjection;
using Suyaa.Hosting.Common.DependencyInjection.Dependency;
using Suyaa.Hosting.Common.DependencyInjection.Helpers;
using Suyaa.Hosting.Mappers;
using System.Reflection;

namespace Suyaa.Hosting.AutoMapper.Helpers
{
    /// <summary>
    /// 容器扩展
    /// </summary>
    public static partial class DependencyManagerHelper
    {
        // 所有注册所有配置
        private static void MapperProfileRegister(MapperProfile profile, Assembly assembly)
        {
            var tps = assembly.GetTypes();
            if (tps is null) return;
            foreach (var tp in tps)
            {
                // 跳过空类型
                if (tp is null) continue;
                // 跳过接口
                if (tp.IsInterface) continue;
                // 跳过抽象类
                if (tp.IsAbstract) continue;
                #region 处理类映射
                // 尝试添加类映射
                profile.TryCreateMapByAttribute(tp);
                #endregion
            }
        }

        // 所有注册所有配置
        private static void MapperProfileRegister(IDependencyManager dependency, MapperProfile profile)
        {
            //Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly assembly in dependency.Assemblies)
            {
                try
                {
                    MapperProfileRegister(profile, assembly);
                }
                catch { }
            }
        }

        /// <summary>
        /// 添加AutoMapper组件支持
        /// </summary>
        /// <param name="dependency"></param>
        /// <returns></returns>
        public static IDependencyManager AddAutoMapper(this IDependencyManager dependency)
        {
            // 建立映射配置文件
            MapperProfile profile = new MapperProfile();
            // 注册程序集
            dependency.Include<MapperProfile>();
            // 注册所有配置
            MapperProfileRegister(dependency, profile);
            // 添加AutoMapper
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(profile);
            });
            var mapper = configuration.CreateMapper();
            dependency.RegisterInstance<IMapper>(mapper);
            dependency.Register<IObjectMapper, ObjectMapper>(Lifetimes.Exclusive);
            return dependency;
        }
    }
}
