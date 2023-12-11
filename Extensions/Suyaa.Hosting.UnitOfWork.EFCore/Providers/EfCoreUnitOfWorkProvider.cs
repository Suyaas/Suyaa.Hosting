using Suyaa.Data.Dependency;
using Suyaa.Hosting.Common.Sessions.Dependency;
using Suyaa.Hosting.UnitOfWork.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Hosting.UnitOfWork.EFCore.Providers
{
    /// <summary>
    /// 交互信息工作单元供应商
    /// </summary>
    public class EfCoreUnitOfWorkProvider : IUnitOfWorkProvider
    {
        #region 依赖注入

        private readonly IDbWorkManager _dbWorkManager;
        private IDbWork? _dbWork;

        /// <summary>
        /// 交互信息工作单元供应商
        /// </summary>
        public EfCoreUnitOfWorkProvider(
            IDbWorkManager dbWorkManager
            )
        {
            _dbWorkManager = dbWorkManager;
        }

        #endregion

        /// <summary>
        /// 完成
        /// </summary>
        public void OnComplete()
        {
            _dbWork?.Commit();
            _dbWorkManager.ReleaseWork();
        }

        /// <summary>
        /// 异步完成
        /// </summary>
        /// <returns></returns>
        public async Task OnCompleteAsync()
        {
            if (_dbWork is null) return;
            await _dbWork.CommitAsync();
            _dbWorkManager.ReleaseWork();
        }

        /// <summary>
        /// 创建
        /// </summary>
        public void OnCreate()
        {
            _dbWorkManager.ReleaseWork();
            _dbWork = _dbWorkManager.CreateWork();
        }
    }
}
