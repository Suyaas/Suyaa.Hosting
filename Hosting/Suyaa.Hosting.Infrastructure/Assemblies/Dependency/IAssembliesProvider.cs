using System.Reflection;

namespace Suyaa.Hosting.Infrastructure.Assemblies.Dependency
{
    /// <summary>
    /// 程序集聚合供应商
    /// </summary>
    public interface IAssembliesProvider
    {
        /// <summary>
        /// 获取程序集
        /// </summary>
        /// <returns></returns>
        IEnumerable<Assembly> GetAssemblies();
    }
}
