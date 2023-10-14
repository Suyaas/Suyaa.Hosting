using Suyaa.DependencyInjection;
using Suyaa.Hosting.Jwt.ActionFilters;
using Suyaa.Hosting.Jwt.Dependency;
using Suyaa.Hosting.Kernel.Dependency;

namespace Suyaa.Hosting.Jwt.Helpers
{
    /// <summary>
    /// 容器扩展
    /// </summary>
    public static partial class DependencyManagerHelper
    {

        /// <summary>
        /// 添加Jwt数据支持
        /// </summary>
        /// <param name="dependency"></param>
        /// <returns></returns>
        public static IDependencyManager AddJwt(this IDependencyManager dependency)
        {
            dependency.AddJwt<JwtDataProvider>();
            return dependency;
        }

        /// <summary>
        /// 添加Jwt数据支持
        /// </summary>
        /// <param name="dependency"></param>
        /// <returns></returns>
        public static IDependencyManager AddJwt<TProvider>(this IDependencyManager dependency)
            where TProvider : class, IJwtDataProvider
        {
            // 注册管理器
            dependency.Register<IJwtManager, JwtManager>(Lifetimes.Transient);
            // 注册数据供应商
            dependency.Register<IJwtDataProvider, TProvider>(Lifetimes.Transient);
            // 注册授权过滤器
            dependency.Register<JwtAuthorizeFilter, JwtAuthorizeFilter>(Lifetimes.Transient);
            return dependency;
        }
    }
}
