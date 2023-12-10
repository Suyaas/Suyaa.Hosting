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
    /// 工作单元管理器
    /// </summary>
    public class UnitOfWorkManager : IUnitOfWorkManager
    {
        // 缓存操作异步对象
        private static readonly AsyncLocal<UnitOfWorkWrapper> _asyncLocal = new AsyncLocal<UnitOfWorkWrapper>();

        #region 依赖注入

        private readonly IDependencyManager _dependencyManager;

        /// <summary>
        /// 工作单元管理器
        /// </summary>
        /// <param name="dependencyManager"></param>
        public UnitOfWorkManager(IDependencyManager dependencyManager)
        {
            _dependencyManager = dependencyManager;
        }

        #endregion

        /// <summary>
        /// 建立一个新的工作单元
        /// </summary>
        public IUnitOfWork Begin()
        {
            lock (_asyncLocal)
            {
                var work = _dependencyManager.ResolveRequired<IUnitOfWork>();
                _asyncLocal.Value = new UnitOfWorkWrapper(work);
                return work;
            }
        }

        /// <summary>
        /// 获取当前工作单元
        /// </summary>
        /// <returns></returns>
        public IUnitOfWork? GetWork()
        {
            if (_asyncLocal.Value is null) return null;
            return _asyncLocal.Value.UnitOfWork;
        }

        /// <summary>
        /// 释放工作单元
        /// </summary>
        public void ReleaseWork()
        {
            var work = GetWork();
            if (work is null) return;
            lock (_asyncLocal)
            {
                // 使用空包裹曾代替现有包裹曾
                _asyncLocal.Value = new UnitOfWorkWrapper();
            }
            work.Dispose();
        }
    }
}
