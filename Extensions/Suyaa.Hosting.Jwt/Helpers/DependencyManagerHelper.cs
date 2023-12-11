using Suyaa.Hosting.Common.DependencyInjection;
using Suyaa.Hosting.Common.DependencyInjection.Dependency;
using Suyaa.Hosting.Common.DependencyInjection.Helpers;
using Suyaa.Hosting.Jwt.ActionFilters;
using Suyaa.Hosting.Jwt.Configures;
using Suyaa.Hosting.Jwt.Dependency;

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
            dependency.AddJwt<JwtDataProvider, JwtData>();
            return dependency;
        }

        /// <summary>
        /// 添加Jwt数据支持
        /// </summary>
        /// <param name="dependency"></param>
        /// <returns></returns>
        public static IDependencyManager AddJwt<TProvider, TData>(this IDependencyManager dependency)
            where TProvider : class, IJwtDataProvider<TData>
            where TData : class, IJwtData, new()
        {
            // 注册程序集
            dependency.Include<JwtData>();
            // 注册管理器
            dependency.Register<IJwtManager<TData>, JwtManager<TData>>(Lifetimes.Transient);
            // 注册数据供应商
            dependency.Register<IJwtBuilder<TData>, JwtBuilder<TData>>(Lifetimes.Transient);
            // 注册数据供应商
            dependency.Register<IJwtDataProvider<TData>, TProvider>(Lifetimes.Transient);
            // 注册授权过滤器
            dependency.Register<JwtAuthorizeFilter<TData>, JwtAuthorizeFilter<TData>>(Lifetimes.Transient);
            return dependency;
        }
    }
}
