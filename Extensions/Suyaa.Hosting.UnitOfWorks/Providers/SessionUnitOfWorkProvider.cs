using Suyaa.Hosting.Common.Sessions.Dependency;
using Suyaa.Hosting.UnitOfWork.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Hosting.UnitOfWork.Providers
{
    /// <summary>
    /// 交互信息工作单元供应商
    /// </summary>
    public class SessionUnitOfWorkProvider : IUnitOfWorkProvider
    {
        #region 依赖注入

        private readonly ISessionManager _sessionManager;

        /// <summary>
        /// 交互信息工作单元供应商
        /// </summary>
        public SessionUnitOfWorkProvider(
            ISessionManager sessionManager
            )
        {
            _sessionManager = sessionManager;
        }

        #endregion

        /// <summary>
        /// 完成
        /// </summary>
        public void OnComplete()
        {
            _sessionManager.ReleaseSession();
        }

        /// <summary>
        /// 异步完成
        /// </summary>
        /// <returns></returns>
        public async Task OnCompleteAsync()
        {
            _sessionManager.ReleaseSession();
            await Task.CompletedTask;
        }

        /// <summary>
        /// 创建
        /// </summary>
        public void OnCreate()
        {
            _sessionManager.ReleaseSession();
            _sessionManager.GetSession();
        }
    }
}
