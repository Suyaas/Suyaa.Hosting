using Microsoft.AspNetCore.Mvc.Filters;
using Suyaa.Hosting.Common.Exceptions;

namespace Suyaa.Hosting.Jwt.Attributes
{
    /// <summary>
    /// 自定义拦截特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public abstract class CustomAttribute : Attribute, IFilterFactory
    {
        private readonly Type _type;

        /// <summary>
        /// 自定义拦截特性
        /// </summary>
        /// <param name="type"></param>
        public CustomAttribute(Type type)
        {
            if(!type.HasInterface<IFilterMetadata>()) throw new UserFriendlyException($"Custom attribute not implemented 'IFilterMetadata' interface.");
            _type = type;
        }

        /// <summary>
        /// 是否重复使用
        /// </summary>
        public bool IsReusable => false;

        /// <summary>
        /// 创建MetaData实例
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            var metaData = serviceProvider.GetService(_type);
            if (metaData is null) throw new UserFriendlyException($"Custom attribute '{_type.Name}' create fail.");
            return (IFilterMetadata)metaData;
        }

        ///// <summary>
        ///// 接口执行
        ///// </summary>
        ///// <param name="context"></param>
        ///// <exception cref="HostFriendlyException"></exception>
        //public override void OnActionExecuting(ActionExecutingContext context)
        //{
        //    // 检测头部信息
        //    var request = context.HttpContext.Request;
        //    if (!request.Headers.ContainsKey(sy.Jwt.TokenName)) throw new HostFriendlyException($"Jwt invalid.");
        //    string token = request.Headers[sy.Jwt.TokenName].ToString();
        //    // 检测Jwt信息
        //    IJwtData info;
        //    try
        //    {
        //        info = (IJwtData)sy.Jwt.GetData(token, _type);
        //    }
        //    catch (HostException ex)
        //    {
        //        throw new HostFriendlyException(ex.Message);
        //    }
        //    if (info.UserId <= 0) throw new HostFriendlyException($"Jwt invalid.");
        //    // 填充信息
        //    context.RouteData.Values[sy.Jwt.RouteName] = info;
        //    base.OnActionExecuting(context);
        //}
    }
}
