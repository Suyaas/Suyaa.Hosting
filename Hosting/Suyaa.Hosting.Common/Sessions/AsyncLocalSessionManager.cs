using Suyaa.Hosting.Common.DependencyInjection.Dependency;
using Suyaa.Hosting.Common.DependencyInjection.Helpers;
using Suyaa.Hosting.Common.Sessions.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Hosting.Common.Sessions
{
    /// <summary>
    /// 本地异步交互信息管理器
    /// </summary>
    public sealed class AsyncLocalSessionManager : ISessionManager
    {
        // 异步数据对象
        private static readonly AsyncLocal<AsyncLocalSessionWrapper> _asyncLocal = new AsyncLocal<AsyncLocalSessionWrapper>();

        #region 依赖注入

        private readonly IDependencyManager _dependencyManager;

        /// <summary>
        /// 本地异步交互信息管理器
        /// </summary>
        public AsyncLocalSessionManager(
            IDependencyManager dependencyManager
            )
        {
            _dependencyManager = dependencyManager;
        }

        #endregion

        /// <summary>
        /// 获取交互信息
        /// </summary>
        /// <returns></returns>
        public ISession GetSession()
        {
            if (_asyncLocal.Value is null || _asyncLocal.Value.Session is null)
            {
                lock (_asyncLocal)
                {
                    var provider = _dependencyManager.ResolveRequired<ISessionProvider>();
                    _asyncLocal.Value = new AsyncLocalSessionWrapper(provider.GetSession());
                }
            }
            return _asyncLocal.Value.Session!;
        }

        /// <summary>
        /// 释放交互信息
        /// </summary>
        public void ReleaseSession()
        {
            lock (_asyncLocal)
            {
                _asyncLocal.Value = new AsyncLocalSessionWrapper();
            }
        }
    }
}
