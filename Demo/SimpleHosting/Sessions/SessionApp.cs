using Suyaa.Hosting.App.Services;
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
        private readonly ISessionManager _sessionManager;

        public SessionApp(
            ISessionManager sessionManager
            )
        {
            _sessionManager = sessionManager;
        }

        /// <summary>
        /// 获取测试值
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetTestValue()
        {
            var session = _sessionManager.GetSession();
            session.Set("test", "123");
            var value = session.Get<string>("test");
            return await Task.FromResult(value) ?? string.Empty;
        }
    }
}
