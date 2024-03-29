﻿using Suyaa.Configure;
using Suyaa.Hosting.Common.Configures.Attributes;

namespace Suyaa.Hosting.Common.Configures
{
    /// <summary>
    /// 主机服务配置
    /// </summary>
    [Configuration("Hosting")]
    public class HostConfig : IConfig
    {
        /// <summary>
        /// 寻址路径集合
        /// </summary>
        public List<string> Paths { get; set; }

        /// <summary>
        /// 程序集设置
        /// </summary>
        public List<string> Libraries { get; set; }

        /// <summary>
        /// 日志路径
        /// </summary>
        public string LogPath { get; set; }

        /// <summary>
        /// 是否记录详细日志
        /// </summary>
        public bool IsVerboseLog { get; set; }

        /// <summary>
        /// 是否支持跨域
        /// </summary>
        public bool IsCorsAll { get; set; }

        /// <summary>
        /// 是否显示Swagger
        /// </summary>
        public bool IsSwagger { get; set; }

        /// <summary>
        /// Swagger配置
        /// </summary>
        public List<HostSwaggerConfig> Swaggers { get; set; }

        /// <summary>
        /// 是否支持页面
        /// </summary>
        public bool IsRazorPageSupported { get; set; }

        /// <summary>
        /// 是否支持控制器
        /// </summary>
        public bool IsControllerSupported { get; set; }

        /// <summary>
        /// 领域服务
        /// </summary>
        public HostDomainService DomainService { get; set; } = new HostDomainService();

        /// <summary>
        /// 舒雅服务配置
        /// </summary>
        public HostConfig()
        {
            // 设置默认值
            Paths = new List<string>();
            Libraries = new List<string>();
            LogPath = string.Empty;
            IsCorsAll = false;
            Swaggers = new List<HostSwaggerConfig>();
        }

        /// <summary>
        /// 获取默认配置
        /// </summary>
        public void Default()
        {
            // 设置默认日志路径
            LogPath = "~/logs";
            // 添加默认路径
            Paths.Add("~/libs");
            // 添加默认的swagger配置
            Swaggers.Add(new HostSwaggerConfig()
            {
                Name = "all",
                Description = "All APIs",
                Keyword = "*"
            });
            this.DomainService.IsSupported = false;
            this.DomainService.RouteRoot = "/app";
        }
    }

    /// <summary>
    /// 主机服务Swagger配置
    /// </summary>
    public sealed class HostSwaggerConfig
    {

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// 过滤关键字
        /// </summary>
        public string Keyword { get; set; } = string.Empty;

    }

    /// <summary>
    /// 领域服务配置
    /// </summary>
    public sealed class HostDomainService
    {

        /// <summary>
        /// 是否支持
        /// </summary>
        public bool IsSupported { get; set; } = false;

        /// <summary>
        /// 路由根路径
        /// </summary>
        public string RouteRoot { get; set; } = string.Empty;

    }
}
