using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Suyaa.Hosting.Options;
using System.Reflection;

namespace Suyaa.Hosting.Kernel.FeatureProviders
{
    /// <summary>
    /// 服务控制器特征提供者
    /// </summary>
    public class ServiceControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>, IApplicationFeatureProvider
    {
        private readonly ServiceOption _option;

        /// <summary>
        /// 服务控制器特征提供者
        /// </summary>
        /// <param name="option"></param>
        public ServiceControllerFeatureProvider(ServiceOption option)
        {
            _option = option;
        }

        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
        {
            foreach (var type in _option.Types)
            {
                var info = type.GetTypeInfo();
                // 添加路由信息
                feature.Controllers.Add(info);
            }
        }
    }
}
