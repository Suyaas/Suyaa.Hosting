using Suyaa.Hosting.Common.ActionFilters;
using Suyaa.Hosting.Common.ActionFilters.Dependency;
using Suyaa.Hosting.Common.DependencyInjection;
using Suyaa.Hosting.Common.DependencyInjection.Dependency;
using Suyaa.Hosting.Common.DependencyInjection.Helpers;
using Suyaa.Hosting.Common.Sessions;
using Suyaa.Hosting.Common.Sessions.Dependency;

namespace Suyaa.Hosting.Sessions.Helpers
{
    /// <summary>
    /// 容器扩展
    /// </summary>
    public static partial class DependencyManagerHelper
    {
        /// <summary>
        /// 添加交互信息支持
        /// </summary>
        /// <param name="dependency"></param>
        /// <returns></returns>
        public static IDependencyManager AddSession(this IDependencyManager dependency)
        {
            dependency.Register<ISessionManager, AsyncLocalSessionManager>(Lifetimes.Transient);
            dependency.Register<ISessionProvider, MemorySessionProvider>(Lifetimes.Transient);
            return dependency;
        }
    }
}
