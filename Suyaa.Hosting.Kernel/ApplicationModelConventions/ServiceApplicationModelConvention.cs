using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Routing;
using Suyaa.Hosting.Kernel.Attributes;
using Suyaa.Hosting.Kernel.Dependency;
using Suyaa.Hosting.Options;
using System.Reflection;

namespace Suyaa.Hosting.Kernel.ApplicationModelConventions
{
    /// <summary>
    /// 服务应用模型提供商
    /// </summary>
    public class ServiceApplicationModelConvention : IApplicationModelConvention
    {
        // 路由设置
        private readonly ServiceOption _option;

        /// <summary>
        /// 服务应用模型提供商
        /// </summary>
        /// <param name="route"></param>
        public ServiceApplicationModelConvention(ServiceOption option)
        {
            _option = option;
        }

        // 应用行为
        private void ApplyControllerAction(ApplicationModel application, ControllerModel controllerModel, string controllerName, ActionModel actionModel)
        {
            // 方法属性
            var method = actionModel.ActionMethod;
            var httpMethod = method.GetCustomAttribute<HttpMethodAttribute>();
            // 兼容系统定义
            if (httpMethod is null) return;
            if (!httpMethod.Template.IsNullOrWhiteSpace()) return;
            // 遍历选择器
            foreach (var selector in actionModel.Selectors)
            {
                if (selector.AttributeRouteModel != null) continue;
                // 判断是否定义了Route
                AttributeRouteModel routeModel = new AttributeRouteModel() { Template = method.Name };
                selector.AttributeRouteModel = routeModel;
                //selector.EndpointMetadata.Add(new HttpGetAttribute());
                //selector.EndpointMetadata.Add(new HttpMethodMetadata(new List<string>() { "GET" }));
            }
        }

        // 应用控制器
        private void ApplyController(ApplicationModel application, ControllerModel controllerModel)
        {
            // 判读是否为应用服务类
            var controllerType = controllerModel.ControllerType;
            if (!controllerType.HasInterface<IServiceApp>()) return;
            // 获取App特性
            string controllerName;
            var app = controllerType.GetCustomAttribute<AppAttribute>();
            if (app is null)
            {
                // 兼容ServerApp结尾
                if (controllerType.Name.EndsWith(Resources.String_ServerApp))
                {
                    controllerName = controllerType.Name.Substring(0, controllerType.Name.Length - Resources.String_ServerApp.Length);
                }
                // 兼容App结尾
                else if (controllerType.Name.EndsWith(Resources.String_App))
                {
                    controllerName = controllerType.Name.Substring(0, controllerType.Name.Length - Resources.String_App.Length);
                }
                // 使用控制器名称
                else
                {
                    controllerName = controllerType.Name;
                }
            }
            else
            {
                controllerName = app.Name;
            }
            // 判断是否定义了Route
            AttributeRouteModel routeModel = new AttributeRouteModel() { Name = controllerName, Template = _option.RouteUrl + "/" + _option.GetModuleName(controllerType) + "/" + controllerName };
            foreach (var selector in controllerModel.Selectors)
            {
                selector.AttributeRouteModel = routeModel;
            }
            // 处理所有行为
            foreach (var action in controllerModel.Actions) ApplyControllerAction(application, controllerModel, controllerName, action);
        }

        /// <summary>
        /// 应用
        /// </summary>
        /// <param name="application"></param>
        public void Apply(ApplicationModel application)
        {
            foreach (var controller in application.Controllers) ApplyController(application, controller);
        }
    }
}
