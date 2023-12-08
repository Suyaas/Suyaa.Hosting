using Suyaa.Data.Dependency;
using Suyaa.Hosting.Data.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Hosting.Data.Managers
{
    /// <summary>
    /// 简单的数据库工作者管理器
    /// </summary>
    public sealed class DbWorkManager : IDbWorkManager
    {
        // 缓存操作异步对象
        private static readonly AsyncLocal<AsyncLocalDbWorkWrapper> _asyncLocal = new AsyncLocal<AsyncLocalDbWorkWrapper>();

        /// <summary>
        /// 简单的数据库工作者管理器
        /// </summary>
        public DbWorkManager(
            IDbFactory factory,
            IDbConnectionDescriptorFactory dbConnectionDescriptorFactory
            )
        {
            Factory = factory;
            ConnectionDescriptor = dbConnectionDescriptorFactory.DefaultConnection;
        }

        /// <summary>
        /// 数据库工厂
        /// </summary>
        public IDbFactory Factory { get; }

        /// <summary>
        /// 连接描述
        /// </summary>
        public IDbConnectionDescriptor ConnectionDescriptor { get; }

        /// <summary>
        /// 创建一个数据库工作者
        /// </summary>
        /// <returns></returns>
        public IDbWork CreateWork()
        {
            var work = Factory.WorkProvider.CreateWork(this);
            SetCurrentWork(work);
            return work;
        }

        /// <summary>
        /// 获取当前工作者
        /// </summary>
        /// <returns></returns>
        public IDbWork? GetCurrentWork()
        {
            if (_asyncLocal.Value is null) return null;
            return _asyncLocal.Value.DbWork;
        }

        /// <summary>
        /// 释放工作者
        /// </summary>
        public void ReleaseWork()
        {
            if (_asyncLocal.Value is null) return;
            if (_asyncLocal.Value.DbWork is null) return;
            var work = _asyncLocal.Value.DbWork;
            lock (_asyncLocal)
            {
                _asyncLocal.Value = new AsyncLocalDbWorkWrapper();
            }
            work.Dispose();
        }

        /// <summary>
        /// 设置当前工作者
        /// </summary>
        /// <param name="work"></param>
        public void SetCurrentWork(IDbWork work)
        {
            // 释放当前作业
            ReleaseWork();
            // 设置新的作业
            lock (_asyncLocal)
            {
                _asyncLocal.Value = new AsyncLocalDbWorkWrapper(work);
            }
        }
    }
}
