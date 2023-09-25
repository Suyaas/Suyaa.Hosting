using AutoMapper;
using Suyaa.Hosting.AutoMapper.Attributes;
using System.Reflection;

namespace Suyaa.Hosting.Mappers
{
    /// <summary>
    /// 映射配置文件
    /// </summary>
    public class MapperProfile : Profile
    {

        // 尝试从源映射特性中创建映射
        private void TryCreateMapByFromAttribute(Type type)
        {
            // 读取特性
            var mapFromAttr = type.GetCustomAttribute<MapFromAttribute>();
            if (mapFromAttr is null) return;
            foreach (var tp in mapFromAttr.Types)
            {
                CreateMap(tp, type);
            }
        }

        // 尝试从目标映射特性中创建映射
        private void TryCreateMapByToAttribute(Type type)
        {
            // 读取特性
            var mapFromAttr = type.GetCustomAttribute<MapToAttribute>();
            if (mapFromAttr is null) return;
            foreach (var tp in mapFromAttr.Types)
            {
                CreateMap(type, tp);
            }
        }

        // 尝试从相互映射特性中创建映射
        private void TryCreateMapByEachAttribute(Type type)
        {
            // 读取特性
            var mapFromAttr = type.GetCustomAttribute<MapEachAttribute>();
            if (mapFromAttr is null) return;
            foreach (var tp in mapFromAttr.Types)
            {
                CreateMap(type, tp);
                CreateMap(tp, type);
            }
        }

        /// <summary>
        /// 尝试从特性中创建映射
        /// </summary>
        /// <param name="type"></param>
        public void TryCreateMapByAttribute(Type type)
        {
            TryCreateMapByEachAttribute(type);
            TryCreateMapByFromAttribute(type);
            TryCreateMapByToAttribute(type);
        }
    }
}
