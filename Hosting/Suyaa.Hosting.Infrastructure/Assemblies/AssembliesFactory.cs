using Suyaa.Hosting.Infrastructure.Assemblies.Dependency;
using System.Reflection;

namespace Suyaa.Hosting.Infrastructure.Assemblies
{
    /// <summary>
    /// 程序集聚合工厂
    /// </summary>
    public sealed class AssembliesFactory : IAssembliesFactory
    {
        // 供应商集合
        private readonly IAssembliesProvider[] _providers;

        /// <summary>
        /// 程序集聚合工厂
        /// </summary>
        /// <param name="providers"></param>
        public AssembliesFactory(params IAssembliesProvider[] providers)
        {
            _providers = providers;
        }

        /// <summary>
        /// 获取程序集聚合
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Assembly> GetAssemblies()
        {
            List<Assembly> assemblies = new List<Assembly>();
            foreach (var provider in _providers)
            {
                assemblies.AddRange(provider.GetAssemblies());
            }
            return assemblies;
        }
    }
}
