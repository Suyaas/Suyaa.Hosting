using Microsoft.Extensions.Primitives;
using System.Threading;
using System;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Suyaa.Hosting.Options;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Patterns;
using Suyaa.Configure.Helpers;
using System.Text;
using System.Reflection;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Suyaa.Hosting.Applications
{
    /// <summary>
    /// 应用路由端点数据源
    /// </summary>
    public sealed class ApplicationEndPointDataSource : EndpointDataSource, IDisposable
    {
        // 配置
        private readonly ApplicationOption _option;
        private readonly IReadOnlyList<ControllerActionDescriptor> _actions;

        // 私有变量
        private List<Endpoint> _endpoints;
        private CancellationTokenSource _cancellationTokenSource;
        private IChangeToken _changeToken;


        /// <summary>
        /// 应用路由端点数据源
        /// </summary>
        public ApplicationEndPointDataSource(ApplicationOption option, IReadOnlyList<ControllerActionDescriptor> actions)
        {
            _option = option;
            _actions = actions;
            _cancellationTokenSource = new CancellationTokenSource();
            _changeToken = new CancellationChangeToken(_cancellationTokenSource.Token);
            _endpoints = new List<Endpoint>();
            // 初始化
            this.Initialize();
        }

        /// <summary>
        /// 端点集合
        /// </summary>
        public override IReadOnlyList<Endpoint> Endpoints => _endpoints;

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            _cancellationTokenSource.Cancel();
        }

        /// <summary>
        /// GetChangeToken
        /// </summary>
        /// <returns></returns>
        public override IChangeToken GetChangeToken() => _changeToken;

        private static async void ResponseContent(HttpContext context, ControllerActionDescriptor action, object? res)
        {
            if (res is null) return;
            if (res is string str)
            {
                await context.Response.Body.WriteAsync(Encoding.UTF8.GetBytes(str));
            }
        }

        // 创建RequestDelegate
        private static RequestDelegate CreateRequestDelegate()
        {
            // We don't want to close over the Invoker Factory in ActionEndpointFactory as
            // that creates cycles in DI. Since we're creating this delegate at startup time
            // we don't want to create all of the things we use at runtime until the action
            // actually matches.
            //
            // The request delegate is already a closure here because we close over
            // the action descriptor.
            IActionInvokerFactory? invokerFactory = null;

            return (context) =>
            {
                var endpoint = context.GetEndpoint()!;
                var dataTokens = endpoint.Metadata.GetMetadata<IDataTokensMetadata>();

                var routeData = new RouteData();
                routeData.PushState(router: null, context.Request.RouteValues, new RouteValueDictionary(dataTokens?.DataTokens));

                // Don't close over the ActionDescriptor, that's not valid for pages.
                var action = endpoint.Metadata.GetMetadata<ActionDescriptor>()!;
                var actionContext = new ActionContext(context, routeData, action);

                if (invokerFactory == null)
                {
                    invokerFactory = context.RequestServices.GetRequiredService<IActionInvokerFactory>();
                }

                var invoker = invokerFactory.CreateInvoker(actionContext);
                return invoker!.InvokeAsync();
            };
        }

        // 创建RequestDelegate
        private static RequestDelegate CreateRequestDelegate(ControllerActionDescriptor action)
        {
            return (context) =>
            {
                var obj = context.RequestServices.GetRequiredService(action.ControllerTypeInfo);
                var res = action.MethodInfo.Invoke(obj, null);
                ResponseContent(context, action, res);
                return Task.CompletedTask;
            };
        }

        // 初始化
        private void Initialize()
        {
            if (_endpoints.Any()) return;
            var endpoints = new List<Endpoint>();
            var requestDelegate = CreateRequestDelegate();
            foreach (var action in _actions)
            {
                //var requestDelegate = CreateRequestDelegate(action);
                var pattern = RoutePatternFactory.Parse(_option.RouteUrl + "/" + action.DisplayName);
                var builder = new RouteEndpointBuilder(requestDelegate, pattern, 0)
                {
                    DisplayName = pattern.RawText
                };
                // 将描述符添加到元数据中
                builder.Metadata.Add(action);
                // 添加所有的特性
                var attributes = action.MethodInfo.GetCustomAttributes();
                foreach (var attribute in attributes)
                {
                    builder.Metadata.Add(attribute);
                }
                //var endpoint = new RouteEndpoint(requestDelegate, EndpointMetadataCollection.Empty, );
                endpoints.Add(builder.Build());
            }
            _endpoints = endpoints;
        }
    }
}
