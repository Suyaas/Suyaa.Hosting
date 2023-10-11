using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Routing;
using Suyaa.Hosting.Kernel.Attributes;
using Suyaa.Hosting.Kernel.Dependency;
using Suyaa.Hosting.Options;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

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
                if (controllerType.Name.EndsWith(Resources.String_ServiceApp))
                {
                    controllerName = controllerType.Name.Substring(0, controllerType.Name.Length - Resources.String_ServiceApp.Length);
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
            //// 处理所有行为
            //foreach (var action in controllerModel.Actions) ApplyControllerAction(application, controllerModel, controllerName, action);
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
