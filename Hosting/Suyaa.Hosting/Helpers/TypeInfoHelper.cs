using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Reflection;

namespace Suyaa.Hosting.Helpers
{
    /// <summary>
    /// 类型信息助手
    /// </summary>
    public static class TypeInfoHelper
    {
        /// <summary>
        /// 获取控制器描述符
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static IReadOnlyList<ControllerActionDescriptor> GetDescriptors(this TypeInfo info)
        {
            var descriptors = new List<ControllerActionDescriptor>();
            var methods = info.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            foreach (var method in methods)
            {
                var descriptor = new ControllerActionDescriptor()
                {
                    ActionName = method.Name,
                    MethodInfo = method,
                    ControllerName = info.Name,
                    ControllerTypeInfo = info,
                    DisplayName = info.Name + "/" + method.Name,
                };
                descriptors.Add(descriptor);
            }
            return descriptors;
        }
    }
}
