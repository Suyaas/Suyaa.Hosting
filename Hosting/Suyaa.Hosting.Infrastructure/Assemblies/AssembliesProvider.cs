using Suyaa.Hosting.Infrastructure.Assemblies.Dependency;
using System.Reflection;

namespace Suyaa.Hosting.Infrastructure.Assemblies
{
    /// <summary>
    /// 程序集聚合供应商
    /// </summary>
    public class AssembliesProvider : IAssembliesProvider
    {
        private readonly IEnumerable<Assembly> _assemblies;

        /// <summary>
        /// 程序集聚合供应商
        /// </summary>
        /// <param name="assemblies"></param>
        public AssembliesProvider(IEnumerable<Assembly> assemblies)
        {
            _assemblies = assemblies;
        }

        /// <summary>
        /// 获取程序集聚合
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Assembly> GetAssemblies()
        {
            return _assemblies;
        }
    }
}
