using Suyaa.Hosting.Common.DependencyInjection.Dependency;
using Suyaa.Hosting.Common.DependencyInjection.Helpers;
using Suyaa.Hosting.UnitOfWork.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Hosting.UnitOfWork
{
    /// <summary>
    /// 工作单元
    /// </summary>
    public sealed class UnitOfWork : Disposable, IUnitOfWork
    {
        #region 依赖注入

        private readonly IDependencyManager _dependencyManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IEnumerable<IUnitOfWorkProvider> _providers;

        /// <summary>
        /// 工作单元
        /// </summary>
        public UnitOfWork(
            IDependencyManager dependencyManager,
            IUnitOfWorkManager unitOfWorkManager
            )
        {
            _dependencyManager = dependencyManager;
            _unitOfWorkManager = unitOfWorkManager;
            _providers = _dependencyManager.Resolves<IUnitOfWorkProvider>();
            this.Create();
        }

        #endregion

        // 创建
        private void Create()
        {
            foreach (var provider in _providers) provider.OnCreate();
        }

        /// <summary>
        /// 完成
        /// </summary>
        public void Complete()
        {
            foreach (var provider in _providers) provider.OnComplete();
        }

        /// <summary>
        /// 异步方式完成
        /// </summary>
        /// <returns></returns>
        public async Task CompleteAsync()
        {
            foreach (var provider in _providers) await provider.OnCompleteAsync();
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        protected override void OnManagedDispose()
        {
            base.OnManagedDispose();
            _unitOfWorkManager.ReleaseWork();
        }
    }
}
