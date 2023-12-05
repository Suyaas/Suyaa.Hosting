using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Suyaa.Hosting.Kernel.Dependency;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Suyaa.Hosting;
using Suyaa.Hosting.Kernel;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection.Metadata;
using System;
using Suyaa.Hosting.Kernel.Enums;
using Microsoft.OpenApi.Models;

namespace Suyaa.Hosting.EFCore.ActionFilters
{
    /// <summary>
    /// EFCore切片供应商
    /// </summary>
    public sealed class RequiredActionFilterProvider : IActionFilterProvider
    {

        #region DI注入

        /// <summary>
        /// EFCore切片供应商
        /// </summary>
        public RequiredActionFilterProvider(
            )
        {
        }

        #endregion

        // 抛出异常
        private UserFriendlyException CreateRequiredException(string name, RequiredAttribute attribute)
        {
            if (attribute.ErrorMessage.IsNullOrWhiteSpace()) return new UserFriendlyException(ErrorCode.Required, "Paramter '{0}' is required", name);
            return new UserFriendlyException(attribute.ErrorMessage!);
        }

        // 抛出异常
        private UserFriendlyException CreateRequiredException(string name)
        {
            return new UserFriendlyException(ErrorCode.Required, "Paramter '{0}' is required", name);
        }

        // 检测参数是否存在
        private bool CheckParamterExists(ControllerParameterDescriptor descriptor, IDictionary<string, object?> args)
        {
            // 判断参数是否存在
            if (!args.ContainsKey(descriptor.Name)) return false;
            // 判断是否为空值
            var value = args[descriptor.Name];
            if (value is null) return false;
            // 判断是否为空字符串
            if (value is string strValue)
            {
                if (strValue.IsNullOrWhiteSpace()) return false;
            }
            return true;
        }

        // 直接参数必填校验
        private void ActionArgumentRequiredVerify(ActionExecutingContext context)
        {
            var action = context.ActionDescriptor;
            var args = context.ActionArguments;
            // 查询所有参数的可配置校验
            foreach (var paramter in action.Parameters)
            {
                if (paramter is not ControllerParameterDescriptor descriptor) continue;
                var attr = descriptor.ParameterInfo.GetCustomAttribute<RequiredAttribute>();
                if (attr is null) continue;
                // 判断参数是否存在
                if (!CheckParamterExists(descriptor, args))
                {
                    throw CreateRequiredException(descriptor.Name, attr);
                }
            }
        }

        // 获取必填属性集合
        private List<Tuple<PropertyInfo, RequiredAttribute>> GetRequiredProperties(Type type)
        {
            List<Tuple<PropertyInfo, RequiredAttribute>> propertyInfos = new List<Tuple<PropertyInfo, RequiredAttribute>>();
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var prop in properties)
            {
                var attr = prop.GetCustomAttribute<RequiredAttribute>();
                if (attr is null) continue;
                propertyInfos.Add(Tuple.Create(prop, attr));
            }
            return propertyInfos;
        }

        // 检测属性是否存在
        private void CheckPropertiesExists(ControllerParameterDescriptor descriptor, object obj, IEnumerable<Tuple<PropertyInfo, RequiredAttribute>> properties)
        {
            foreach (var prop in properties)
            {
                var value = prop.Item1.GetValue(obj);
                if (value is null) throw CreateRequiredException(prop.Item1.Name, prop.Item2);
                // 判断是否为空字符串
                if (value is string strValue)
                {
                    if (strValue.IsNullOrWhiteSpace()) throw CreateRequiredException(prop.Item1.Name, prop.Item2);
                }
            }
        }

        // 对象参数必填校验
        private void ActionObjectArgumentRequiredVerify(ActionExecutingContext context)
        {
            var action = context.ActionDescriptor;
            var args = context.ActionArguments;
            // 查询所有参数的可配置校验
            foreach (var paramter in action.Parameters)
            {
                if (paramter is not ControllerParameterDescriptor descriptor) continue;
                // 参数为类型，则退出
                var parameterType = descriptor.ParameterType;
                if (parameterType.IsValueType) return;
                if (parameterType.IsBased<string>()) return;
                // 获取所有的必填字段
                var properties = GetRequiredProperties(parameterType);
                if (!properties.Any()) continue;
                // 获取参数对象
                var obj = args[descriptor.Name];
                if (obj is null) throw CreateRequiredException(descriptor.Name);
                // 检测对象属性
                CheckPropertiesExists(descriptor, obj, properties);
            }
        }

        /// <summary>
        /// 执行开始
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // 直接参数唯一性校验
            ActionArgumentRequiredVerify(context);
            // 对象参数唯一性校验
            ActionObjectArgumentRequiredVerify(context);
        }

        /// <summary>
        /// 执行结束
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
