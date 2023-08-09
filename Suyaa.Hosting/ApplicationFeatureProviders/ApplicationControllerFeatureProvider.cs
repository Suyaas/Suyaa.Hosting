using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Suyaa.Hosting.Dependency;
using Suyaa.Hosting.Options;
using System.Collections.ObjectModel;
using System.Reflection;

namespace Suyaa.Hosting.ApplicationFeatureProviders
{
    /// <summary>
    /// 应用控制器注册
    /// </summary>
    public class ApplicationControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>, IApplicationFeatureProvider
    {
        private readonly ApplicationOption _option;

        /// <summary>
        /// 应用控制器注册
        /// </summary>
        public ApplicationControllerFeatureProvider(ApplicationOption option)
        {
            _option = option;
        }

        /// <summary>
        /// 注册应用
        /// </summary>
        /// <param name="parts"></param>
        /// <param name="feature"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
        {
            foreach (var type in _option.Types)
            {
                var info = type.GetTypeInfo();
                // 获取原有的ApiController特性
                var apiController = info.GetCustomAttribute<ApiControllerAttribute>();
                // 添加路由信息
                feature.Controllers.Add(info);
            }
        }
    }
}
