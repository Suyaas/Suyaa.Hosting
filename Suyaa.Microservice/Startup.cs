using Egg;
using Egg.Log.Loggers;
using Microsoft.Extensions.Configuration;
using Suyaa.Microservice.Configures;
using Suyaa.Microservice.Exceptions;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Suyaa.Microservice
{
    /// <summary>
    /// 启动器
    /// </summary>
    public class Startup
    {
        // 读取配置
        private SuyaaSetting ReadSuyaaSetting(string path)
        {
            try
            {
                // 不存在时创建配置文件
                if (!egg.IO.CheckFileExists(path))
                {
                    string json = JsonSerializer.Serialize(new SuyaaSetting());
                    egg.IO.WriteUtf8FileContent(path, json);
                }
                // 读取文件
                string content = egg.IO.ReadUtf8FileContent(path);
                return JsonSerializer.Deserialize<SuyaaSetting>(content) ?? new SuyaaSetting();
            }
            catch
            {
                return new SuyaaSetting();
            }
        }

        // 加载库文件
        private void ImportLibrary(string path)
        {
            // 所有所有路径
            for (int i = 0; i < this.Paths.Count; i++)
            {
                string filePath = egg.IO.GetOSPathFormat(this.Paths[i] + path);
                if (egg.IO.CheckFileExists(filePath))
                {
                    Import(Assembly.LoadFrom(filePath));
                }
            }
        }

        // 获取完整地址
        private string GetFullPath(string path)
        {
            if (path.StartsWith("~/"))
            {
                return egg.IO.GetExecutionPath(path.Substring(2));
            }
            else
            {
                return path;
            }
        }

        #region [=====继承方法=====]

        /// <summary>
        /// 初始化事件
        /// </summary>
        /// <param name="configuration"></param>
        protected virtual void OnInitialize() { }

        /// <summary>
        /// 服务注册事件
        /// </summary>
        /// <param name="configuration"></param>
        protected virtual void OnConfigureServices(IServiceCollection services) { }

        /// <summary>
        /// 配置HTTP请求管道事件
        /// </summary>
        /// <param name="configuration"></param>
        protected virtual void OnConfigure(IApplicationBuilder app, IWebHostEnvironment env) { }

        #endregion

        #region [=====公共方法=====]

        /// <summary>
        /// 舒雅服务配置
        /// </summary>
        public SuyaaSetting SuyaaSetting { get; }

        /// <summary>
        /// 寻址路径集合
        /// </summary>
        public List<string> Paths { get; set; }

        /// <summary>
        /// 获取程序集合
        /// </summary>
        public List<Assembly> Assembles { get; }

        /// <summary>
        /// 获取配置接口
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// 导入程序集
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public Startup Import(Assembly? assembly)
        {
            if (assembly is null) return this;
            if (!this.Assembles.Contains(assembly)) this.Assembles.Add(assembly);
            return this;
        }

        /// <summary>
        /// 导入程序集
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public Startup Import(string path)
        {
            ImportLibrary(path);
            return this;
        }

        /// <summary>
        /// 导入程序集
        /// </summary>
        /// <param name="tp"></param>
        /// <returns></returns>
        public Startup Import(Type? tp)
        {
            return Import(tp?.Assembly);
        }

        /// <summary>
        /// 导入程序集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Startup Import<T>() where T : class
        {
            return Import(typeof(T).Assembly);
        }

        /// <summary>
        /// 启动器
        /// </summary>
        /// <param name="configuration">配置</param>
        public Startup(IConfiguration configuration)
        {
            // 属性设置
            this.Configuration = configuration;
            this.Assembles = new List<Assembly>();
            // 加载配置
            var suyaa = configuration.GetSection("Suyaa");
            if (suyaa is null) throw new SuyaaException($"未找到'Suyaa'配置节点");
            string suyaaPath = suyaa.GetValue<string>("Path");
            if (suyaa is null) throw new SuyaaException($"未找到'Suyaa.Path'配置项");
            this.SuyaaSetting = ReadSuyaaSetting(egg.IO.GetExecutionPath(suyaaPath));
            // 注册日志
            egg.Logger.Reg(new FileLogger(GetFullPath(this.SuyaaSetting.LogPath)));
            egg.Logger.Debug($"服务启动 ...");
            // 预处理寻址路径
            this.Paths = new List<string>();
            this.Paths.Add(egg.Assembly.ExecutionDirectory);
            foreach (var path in this.SuyaaSetting.Paths) this.Paths.Add(GetFullPath(path));
            // 触发初始化事件
            this.OnInitialize();
            // 加载所有的程序集
            for (int i = 0; i < this.SuyaaSetting.Libraries.Count; i++)
            {
                ImportLibrary(this.SuyaaSetting.Libraries[i]);
            }
            //// 启动器初始化
            //this.Configurator = new TStartupConfigurator();
            //this.Options = new StartupOptions();
            //// 添加自身
            //this.Options.Import(Assembly.GetExecutingAssembly());
            //// 添加公共组件
            //this.Options.Import<Salesgirl.Common.App.ModulerStartup>();
            //// 启动器启动配置
            //this.Configurator.Configure(this.Options);
        }

        /// <summary>
        ///  注册服务到容器中
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {

        }

        /// <summary>
        /// 配置HTTP请求管道
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

        }

        #endregion

    }
}
