using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Reflection;

namespace Suyaa.Hosting.Kernel.ApplicationModelConventions
{
    /// <summary>
    /// 服务行为约定器
    /// </summary>
    public class ServiceActionModelConvention : IActionModelConvention
    {
        /// <summary>
        /// 生效
        /// </summary>
        /// <param name="action"></param>
        public void Apply(ActionModel action)
        {
            // 方法属性
            var method = action.ActionMethod;
            var httpMethod = method.GetCustomAttribute<HttpMethodAttribute>();
            // 兼容系统定义
            if (httpMethod != null)
            {
                if (!httpMethod.Template.IsNullOrWhiteSpace()) return;
            }
            // 遍历选择器
            foreach (var selector in action.Selectors)
            {
                if (selector.AttributeRouteModel != null) continue;
                AttributeRouteModel routeModel;
                var httpMethodMetadata = selector.EndpointMetadata.Where(d => d is HttpMethodMetadata).FirstOrDefault();
                if (httpMethodMetadata is null)
                {
                    string methodName = method.Name.ToLower();
                    // 自动Get方式
                    if (methodName.StartsWith(Resources.String_Get_Lower))
                    {
                        selector.EndpointMetadata.Add(new HttpMethodMetadata(new List<string>() { Resources.String_Get_Upper }));
                        //selector.EndpointMetadata.Add(new HttpGetAttribute("[action]"));
                        routeModel = new AttributeRouteModel(new HttpGetAttribute()) { Template = method.Name };
                        selector.AttributeRouteModel = routeModel;
                        selector.ActionConstraints.Add(new HttpMethodActionConstraint(new List<string>() { Resources.String_Get_Upper }));
                        continue;
                    }
                    // 自动Put方式
                    if (methodName.StartsWith(Resources.String_Update_Lower))
                    {
                        selector.EndpointMetadata.Add(new HttpMethodMetadata(new List<string>() { Resources.String_PUT_Upper }));
                        //selector.EndpointMetadata.Add(new HttpGetAttribute("[action]"));
                        routeModel = new AttributeRouteModel(new HttpGetAttribute()) { Template = method.Name };
                        selector.AttributeRouteModel = routeModel;
                        selector.ActionConstraints.Add(new HttpMethodActionConstraint(new List<string>() { Resources.String_PUT_Upper }));
                        continue;
                    }
                    // 自动Delete方式
                    if (methodName.StartsWith(Resources.String_Delete_Lower))
                    {
                        selector.EndpointMetadata.Add(new HttpMethodMetadata(new List<string>() { Resources.String_Delete_Upper }));
                        //selector.EndpointMetadata.Add(new HttpGetAttribute("[action]"));
                        routeModel = new AttributeRouteModel(new HttpGetAttribute()) { Template = method.Name };
                        selector.AttributeRouteModel = routeModel;
                        selector.ActionConstraints.Add(new HttpMethodActionConstraint(new List<string>() { Resources.String_Delete_Upper }));
                        continue;
                    }
                    // 默认为Post方式
                    selector.EndpointMetadata.Add(new HttpMethodMetadata(new List<string>() { Resources.String_Post_Upper }));
                    //selector.EndpointMetadata.Add(new HttpGetAttribute("[action]"));
                    routeModel = new AttributeRouteModel(new HttpGetAttribute()) { Template = method.Name };
                    selector.AttributeRouteModel = routeModel;
                    selector.ActionConstraints.Add(new HttpMethodActionConstraint(new List<string>() { Resources.String_Post_Upper }));
                    continue;
                }
                // 判断是否定义了Route
                routeModel = new AttributeRouteModel() { Template = method.Name };
                selector.AttributeRouteModel = routeModel;
                //selector.EndpointMetadata.Add(new HttpGetAttribute());
                //selector.EndpointMetadata.Add(new HttpMethodMetadata(new List<string>() { "GET" }));
            }
        }
    }
}
