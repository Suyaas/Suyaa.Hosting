using Suyaa.Hosting.App.Dependency;
using Suyaa.Hosting.Kernel.Attributes;
using Suyaa.Hosting.Kernel.Dependency;
using System.Reflection;

namespace Suyaa.Hosting.App.Options
{
    /// <summary>
    /// 应用服务配置
    /// </summary>
    public sealed class ServiceOption
    {
        // 模块定义
        private readonly Dictionary<Type, string> _modules;

        /// <summary>
        /// 路由地址
        /// </summary>
        public string RouteUrl { get; set; }

        /// <summary>
        /// 路由地址
        /// </summary>
        public string DefModuleName { get; set; } = "def";

        /// <summary>
        /// 类型集合
        /// </summary>
        public List<Type> Types { get; set; }

        /// <summary>
        /// 添加程序集列表
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public ServiceOption AddAssembly(Assembly assembly)
        {
            // 获取模块名定义
            var moduleName = string.Empty;
            var assemblyModule = assembly.GetCustomAttribute<ModuleAttribute>();
            if (assemblyModule != null) moduleName = assemblyModule.Name;
            // 定位类型
            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                if (!type.HasInterface<IDomainServiceApp>()) continue;
                Types.Add(type);
                // 添加模块名称
                if (!moduleName.IsNullOrWhiteSpace()) _modules.Add(type, moduleName);
            }
            return this;
        }

        /// <summary>
        /// 添加程序集列表
        /// </summary>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public ServiceOption AddAssemblies(List<Assembly> assemblies)
        {
            foreach (var assembly in assemblies) AddAssembly(assembly);
            return this;
        }

        /// <summary>
        /// 获取模块名称
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public string GetModuleName(Type type)
        {
            if (_modules.ContainsKey(type)) return _modules[type];
            return DefModuleName;
        }

        /// <summary>
        /// 应用服务配置
        /// </summary>
        public ServiceOption()
        {
            RouteUrl = string.Empty;
            Types = new List<Type>();
            _modules = new Dictionary<Type, string>();
        }
    }
}
