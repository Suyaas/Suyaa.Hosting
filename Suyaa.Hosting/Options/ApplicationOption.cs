using Suyaa.Hosting.Kernel.Dependency;
using System.Reflection;

namespace Suyaa.Hosting.Options
{
    /// <summary>
    /// 应用服务配置
    /// </summary>
    public sealed class ApplicationOption
    {
        /// <summary>
        /// 路由地址
        /// </summary>
        public string RouteUrl { get; set; }

        /// <summary>
        /// 类型集合
        /// </summary>
        public List<Type> Types { get; set; }

        /// <summary>
        /// 添加程序集列表
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public ApplicationOption AddAssembly(Assembly assembly)
        {
            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                if (!type.HasInterface<IServiceApp>()) continue;
                this.Types.Add(type);
            }
            return this;
        }

        /// <summary>
        /// 添加程序集列表
        /// </summary>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public ApplicationOption AddAssemblies(List<Assembly> assemblies)
        {
            foreach (var assembly in assemblies) AddAssembly(assembly);
            return this;
        }

        /// <summary>
        /// 应用服务配置
        /// </summary>
        public ApplicationOption()
        {
            RouteUrl = string.Empty;
            Types = new List<Type>();
        }
    }
}
