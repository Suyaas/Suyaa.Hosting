using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using Suyaa.Hosting.Infrastructure;
using System.Reflection;

namespace Suyaa.Hosting.App.ModelConventions
{
    /// <summary>
    /// 服务行为约定器
    /// </summary>
    public class ServiceActionModelConvention : IActionModelConvention
    {
        // 应用参数处理
        private void ApplyParameter(ActionModel action)
        {
            // 无参数，则退出
            if (!action.Parameters.Any()) return;
            // 非唯一参数，则退出
            if (action.Parameters.Count > 1) return;
            var parameter = action.Parameters.First();
            // 已有绑定定义，则退出
            if (parameter.BindingInfo != null) return;
            var parameterType = parameter.ParameterType;
            // 参数为类型，则退出
            if (parameterType.IsValueType) return;
            if (parameterType.IsBased<string>()) return;
            // 默认使用Body方式绑定
            parameter.BindingInfo = new BindingInfo()
            {
                BindingSource = BindingSource.Body
            };
            // 方法属性
            var method = action.ActionMethod;
            // 将Get转为Post
            var httpMethod = method.GetCustomAttribute<HttpMethodAttribute>();
            // 兼容系统定义
            if (httpMethod != null)
            {
                if (!httpMethod.Template.IsNullOrWhiteSpace()) return;
            }
            // 遍历选择器
            foreach (var selector in action.Selectors)
            {
                if (selector.AttributeRouteModel is null) continue;
                AttributeRouteModel routeModel = selector.AttributeRouteModel;
                var httpMethodMetadata = (HttpMethodMetadata?)selector.EndpointMetadata.Where(d => d is HttpMethodMetadata).FirstOrDefault();
                if (httpMethodMetadata is null) continue;
                // 判断是否为GET
                if (httpMethodMetadata.HttpMethods.Contains(Resources.Strings.String_Get_Upper))
                {
                    selector.EndpointMetadata.Clear();
                    selector.ActionConstraints.Clear();
                    // 添加Post方式
                    selector.EndpointMetadata.Add(new HttpMethodMetadata(new List<string>() { Resources.Strings.String_Post_Upper }));
                    selector.ActionConstraints.Add(new HttpMethodActionConstraint(new List<string>() { Resources.Strings.String_Post_Upper }));
                    continue;
                }
                // 判断是否定义了Route
                routeModel = new AttributeRouteModel() { Template = method.Name };
                selector.AttributeRouteModel = routeModel;
                //selector.EndpointMetadata.Add(new HttpGetAttribute());
                //selector.EndpointMetadata.Add(new HttpMethodMetadata(new List<string>() { "GET" }));
            }
        }

        // 应用方法接口
        private void ApplyMethod(ActionModel action)
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
                    if (methodName.StartsWith(Resources.Strings.String_Get_Lower))
                    {
                        selector.EndpointMetadata.Add(new HttpMethodMetadata(new List<string>() { Resources.Strings.String_Get_Upper }));
                        //selector.EndpointMetadata.Add(new HttpGetAttribute("[action]"));
                        routeModel = new AttributeRouteModel(new HttpGetAttribute()) { Template = method.Name };
                        selector.AttributeRouteModel = routeModel;
                        selector.ActionConstraints.Add(new HttpMethodActionConstraint(new List<string>() { Resources.Strings.String_Get_Upper }));
                        continue;
                    }
                    // 自动Put方式
                    if (methodName.StartsWith(Resources.Strings.String_Update_Lower))
                    {
                        selector.EndpointMetadata.Add(new HttpMethodMetadata(new List<string>() { Resources.Strings.String_PUT_Upper }));
                        //selector.EndpointMetadata.Add(new HttpGetAttribute("[action]"));
                        routeModel = new AttributeRouteModel(new HttpGetAttribute()) { Template = method.Name };
                        selector.AttributeRouteModel = routeModel;
                        selector.ActionConstraints.Add(new HttpMethodActionConstraint(new List<string>() { Resources.Strings.String_PUT_Upper }));
                        continue;
                    }
                    // 自动Delete方式
                    if (methodName.StartsWith(Resources.Strings.String_Delete_Lower))
                    {
                        selector.EndpointMetadata.Add(new HttpMethodMetadata(new List<string>() { Resources.Strings.String_Delete_Upper }));
                        //selector.EndpointMetadata.Add(new HttpGetAttribute("[action]"));
                        routeModel = new AttributeRouteModel(new HttpGetAttribute()) { Template = method.Name };
                        selector.AttributeRouteModel = routeModel;
                        selector.ActionConstraints.Add(new HttpMethodActionConstraint(new List<string>() { Resources.Strings.String_Delete_Upper }));
                        continue;
                    }
                    // 默认为Post方式
                    selector.EndpointMetadata.Add(new HttpMethodMetadata(new List<string>() { Resources.Strings.String_Post_Upper }));
                    //selector.EndpointMetadata.Add(new HttpGetAttribute("[action]"));
                    routeModel = new AttributeRouteModel(new HttpGetAttribute()) { Template = method.Name };
                    selector.AttributeRouteModel = routeModel;
                    selector.ActionConstraints.Add(new HttpMethodActionConstraint(new List<string>() { Resources.Strings.String_Post_Upper }));
                    continue;
                }
                // 判断是否定义了Route
                routeModel = new AttributeRouteModel() { Template = method.Name };
                selector.AttributeRouteModel = routeModel;
                //selector.EndpointMetadata.Add(new HttpGetAttribute());
                //selector.EndpointMetadata.Add(new HttpMethodMetadata(new List<string>() { "GET" }));
            }
        }

        /// <summary>
        /// 生效
        /// </summary>
        /// <param name="action"></param>
        public void Apply(ActionModel action)
        {
            // 应用方法接口
            ApplyMethod(action);
            // 应用参数处理
            ApplyParameter(action);
        }
    }
}
