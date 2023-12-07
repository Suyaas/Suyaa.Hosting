using System.Reflection;

namespace Suyaa.Hosting.Infrastructure.Assemblies.Dependency
{
    /// <summary>
    /// 程序集聚合工厂
    /// </summary>
    public interface IAssembliesFactory
    {
        /// <summary>
        /// 获取程序集聚合
        /// </summary>
        /// <returns></returns>
        IEnumerable<Assembly> GetAssemblies();
    }
}
