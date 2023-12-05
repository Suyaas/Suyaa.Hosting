using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Suyaa.DependencyInjection;
using Suyaa.Hosting.Kernel.ActionFilters;
using Suyaa.Hosting.Kernel.ApplicationModelConventions;
using Suyaa.Hosting.Kernel.FeatureProviders;
using Suyaa.Hosting.Kernel;
using Suyaa.Hosting.Kernel.WebApplicationProviders;
using Suyaa.Hosting.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;
using Suyaa.Hosting.Configures;

namespace Suyaa.Hosting.WebApplicationProviders
{
    /// <summary>
    /// 简洁应用供应商
    /// </summary>
    public abstract class NeatApplicationProvider : KernelApplicationProvider
    {

        /// <summary>
        /// 服务配置
        /// </summary>
        /// <param name="services"></param>
        public override void OnConfigureServices(IServiceCollection services)
        {
            base.OnConfigureServices(services);

            // 输出服务注册日志
            sy.Logger.Debug($"Neat services configure start ...", "Services");

            #region 添加跨域支持
            if (HostConfig.IsCorsAll)
            {
                services.AddCors(d =>
                {
                    d.AddPolicy(CrosTypes.ALL, p =>
                    {
                        p
                        .SetIsOriginAllowed((host) => true)
                        .AllowAnyHeader()
                        .AllowCredentials()
                        .AllowAnyMethod();
                    });
                });
            }
            #endregion

            // 输出服务注册日志
            sy.Logger.Debug($"Neat services configure completed.", "Services");
        }

        /// <summary>
        /// 应用配置
        /// </summary>
        /// <param name="app"></param>
        /// <exception cref="HostException"></exception>
        public override void OnConfigureApplication(WebApplication app)
        {
            base.OnConfigureApplication(app);
            #region 跨域支持
            // 添加跨域支持
            if (base.HostConfig.IsCorsAll) app.UseCors(CrosTypes.ALL);
            #endregion
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="builder"></param>
        public override void OnInitialize(WebApplicationBuilder builder)
        {
            base.OnInitialize(builder);

            sy.Logger.Debug($"Neat server start ...", LogEvents.Server);

            #region 多语言配置
            sy.Logger.Debug("Load i18n ...");
            // 加载多语言配置
            var i18nSection = base.Configuration.GetSection("i18n");
            if (i18nSection is null) throw new HostException($"Configuration section 'i18n' not found.");
            var i18nPath = i18nSection.GetValue<string>("path");
            if (i18nPath.IsNullOrWhiteSpace()) throw new HostException($"Configuration setting 'i18n.path' not found.");
            string i18nFolder = i18nPath;
            if (i18nFolder.StartsWith("./"))
            {
                i18nFolder = sy.IO.GetExecutionPath(i18nPath.Substring(2));
            }
            if (i18nFolder.StartsWith("~/"))
            {
                i18nFolder = sy.IO.GetWorkPath(i18nPath.Substring(2));
            }
            sy.IO.CreateFolder(i18nFolder);
            var i18nName = i18nSection.GetValue<string>("language");
            if (i18nName.IsNullOrWhiteSpace()) throw new HostException($"Configuration setting 'i18n.language' not found.");
            #endregion

            sy.Logger.Debug($"Neat server completed.", LogEvents.Server);
        }
    }
}
