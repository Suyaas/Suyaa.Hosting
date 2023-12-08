using Suyaa.Hosting.App.Services;
using Suyaa.Hosting.Common.DependencyInjection.Dependency;
using Suyaa.Hosting.Common.DependencyInjection.Helpers;
using Suyaa.Hosting.Common.Sessions.Dependency;
using Suyaa.Hosting.Common.Sessions.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHosting.Sessions
{
    /// <summary>
    /// 交互信息
    /// </summary>
    public sealed class SessionApp : DomainServiceApp
    {
        private readonly IDependencyManager _dependencyManager;

        public SessionApp(
            IDependencyManager dependencyManager
            )
        {
            _dependencyManager = dependencyManager;
        }


        private async Task SetSessionValue()
        {
            var sessionManager = _dependencyManager.Resolve<ISessionManager>();
            var session = sessionManager.GetSession();
            session.Set("test", "123222");
            await Task.CompletedTask;
        }

        /// <summary>
        /// 获取测试值
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetTestValue()
        {
            await SetSessionValue();
            var sessionManager = _dependencyManager.Resolve<ISessionManager>();
            var session = sessionManager.GetSession();
            var value = session.Get<string>("test");
            return value ?? string.Empty;
        }
    }
}
