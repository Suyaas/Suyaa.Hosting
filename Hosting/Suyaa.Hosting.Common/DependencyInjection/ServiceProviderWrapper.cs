using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Hosting.Common.DependencyInjection
{
    /// <summary>
    /// 服务提供商包裹层
    /// </summary>
    public class ServiceProviderWrapper
    {
        /// <summary>
        /// 服务提供商包裹层
        /// </summary>
        /// <param name="serviceProvider"></param>
        public ServiceProviderWrapper(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        /// <summary>
        /// 服务提供商
        /// </summary>
        public IServiceProvider ServiceProvider { get; }
    }
}
