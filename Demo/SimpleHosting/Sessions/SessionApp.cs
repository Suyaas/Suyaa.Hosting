using Suyaa.Hosting.App.Services;
using Suyaa.Hosting.Common.DependencyInjection.Dependency;
using Suyaa.Hosting.Common.DependencyInjection.Helpers;
using Suyaa.Hosting.Common.Sessions.Dependency;
using Suyaa.Hosting.Common.Sessions.Helpers;
using Suyaa.Hosting.UnitOfWork.Dependency;
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
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public SessionApp(
            IDependencyManager dependencyManager,
            IUnitOfWorkManager unitOfWorkManager
            )
        {
            _dependencyManager = dependencyManager;
            _unitOfWorkManager = unitOfWorkManager;
        }


        private async Task SetSessionValue()
        {
            var sessionManager = _dependencyManager.ResolveRequired<ISessionManager>();
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
            var sessionManager = _dependencyManager.ResolveRequired<ISessionManager>();
            var session1 = sessionManager.GetSession();
            string value2;
            using (_unitOfWorkManager.Begin())
            {
                var session2 = sessionManager.GetSession();
                value2 = session2.Get<string>("test") ?? string.Empty;
            }
            var value1 = session1.Get<string>("test");
            return $"v1:{value1}, v2:{value2}";
        }
    }
}
