using Microsoft.Extensions.DependencyInjection;
using Suyaa.Hosting.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Hosting.Test
{
    /// <summary>
    /// 模块启动对象
    /// </summary>
    public class ModuleStartup : IModuleStartup
    {
        /// <summary>
        /// 模块初始化配置
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            //throw new NotImplementedException();
            services.AddModulerIoc(Assembly.GetExecutingAssembly());
        }
    }
}
