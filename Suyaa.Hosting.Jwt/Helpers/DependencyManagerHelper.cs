using Suyaa.DependencyInjection;
using Suyaa.Hosting.Kernel.Dependency;

namespace Suyaa.Hosting.Jwt.Helpers
{
    /// <summary>
    /// 容器扩展
    /// </summary>
    public static partial class DependencyManagerHelper
    {

        /// <summary>
        /// 添加AutoMapper组件支持
        /// </summary>
        /// <param name="dependency"></param>
        /// <returns></returns>
        public static IDependencyManager AddJwt(this IDependencyManager dependency)
        {
            dependency.Register<IJwtData, JwtData>(Lifetimes.Transient);
            dependency.Register<IJwtManager, JwtManager>(Lifetimes.Transient);
            return dependency;
        }
    }
}
