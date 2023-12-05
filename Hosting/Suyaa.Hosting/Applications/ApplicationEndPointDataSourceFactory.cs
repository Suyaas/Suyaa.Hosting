using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Suyaa.Hosting.Helpers;
using Suyaa.Hosting.Options;
using System.Reflection;

namespace Suyaa.Hosting.Applications
{
    /// <summary>
    /// 应用路由端点数据源工厂
    /// </summary>
    public sealed class ApplicationEndPointDataSourceFactory
    {
        // 配置
        private readonly ApplicationOption _option;
        private List<TypeInfo> Controllers { get; }

        /// <summary>
        /// 应用路由端点数据源工厂
        /// </summary>
        public ApplicationEndPointDataSourceFactory(ApplicationOption option)
        {
            _option = option;
            this.Controllers = GetTypeInfos(option.Types);
        }

        // 获取类型信息集合
        private List<TypeInfo> GetTypeInfos(List<Type> types)
        {
            List<TypeInfo> infos = new List<TypeInfo>();
            foreach (Type type in types)
            {
                infos.Add(type.GetTypeInfo());
            }
            return infos;
        }

        /// <summary>
        /// 创建一个新的数据源
        /// </summary>
        /// <returns></returns>
        public ApplicationEndPointDataSource Create()
        {
            // 生成描述器列表
            List<ControllerActionDescriptor> descriptors = new List<ControllerActionDescriptor>();
            foreach (var controller in this.Controllers)
            {
                descriptors.AddRange(controller.GetDescriptors());
            }
            return new ApplicationEndPointDataSource(_option, descriptors);
        }
    }
}
