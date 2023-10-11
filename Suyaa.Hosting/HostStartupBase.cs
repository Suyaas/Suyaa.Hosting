using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Suyaa.Hosting.Configures;
using Suyaa.Hosting.Implementations;
using Suyaa.Hosting.Kernel;
using Suyaa.Hosting.Kernel.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Hosting
{
    /// <summary>
    /// 主机启动器
    /// </summary>
    public abstract class HostStartupBase : Kernel.StartupBase
    {
        /// <summary>
        /// 主机启动器
        /// </summary>
        /// <param name="configuration"></param>
        public HostStartupBase(IConfiguration configuration) : base(configuration)
        {
            #region 多语言配置
            sy.Logger.Debug("Load i18n ...");
            // 加载多语言配置
            var i18nSection = configuration.GetSection("i18n");
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
            //this.I18nConfigManager = new JsonConfigManager<I18nConfig>(sy.IO.CombinePath(i18nFolder, i18nName + ".json"));
            //_i18n = new(this.I18nConfigManager);
            #endregion
        }

        /// <summary>
        /// 服务配置
        /// </summary>
        /// <param name="services"></param>
        public override void ConfigureServices(IServiceCollection services)
        {
            #region 添加Swagger配置
            if (base.HostConfig.IsSwagger)
            {
                services.AddSwaggerGen(options =>
                {
                    foreach (var swagger in base.HostConfig.Swaggers)
                    {
                        options.SwaggerDoc(swagger.Name, new OpenApiInfo { Title = swagger.Description, Version = swagger.Name });
                    }
                    var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                    // 将所有xml文档添加到Swagger
                    var directory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
                    var files = directory.GetFiles("*.xml");
                    foreach (var file in files)
                    {
                        //if (swagger.Keyword == "*" || file.Name.Contains(swagger.Keyword))
                        options.IncludeXmlComments(file.FullName, true);
                    }
                    options.DocInclusionPredicate((docName, description) =>
                    {
                        var swagger = base.HostConfig.Swaggers.Where(d => d.Name == docName).FirstOrDefault();
                        if (swagger is null) return false;
                        if (swagger.Keyword == "*") return true;
                        if (description.RelativePath is null) return false;
                        if (description.ActionDescriptor is null) return false;
                        string? displayName = description.ActionDescriptor.DisplayName;
                        if (displayName is null) return false;
                        if (displayName.StartsWith(swagger.Keyword)) return true;
                        return false;
                    });
                });
            }
            #endregion

            // 注册仓库
            services.AddTransient(typeof(IRepository<,>), typeof(Repository<,>));

            base.ConfigureServices(services);
        }

        /// <summary>
        /// 应用配置
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            #region 添加Swagger支持
            if (base.HostConfig.IsSwagger)
            {
                // 使用Swagger
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    foreach (var swagger in base.HostConfig.Swaggers)
                    {
                        options.SwaggerEndpoint($"/swagger/{swagger.Name}/swagger.json", swagger.Description);
                    }
                    //options.SwaggerEndpoint("/swagger/v1/swagger.json", "Suyaa Microservice API V1");
                    options.EnableFilter();
                });
            }
            #endregion

            base.Configure(app, env);
        }
    }
}
