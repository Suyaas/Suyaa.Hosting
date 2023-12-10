using Microsoft.AspNetCore.Mvc.Filters;
using Suyaa.Hosting.Common.ActionFilters.Dependency;
using Suyaa.Hosting.EFCore.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Hosting.EFCore.ActionFilters
{
    /// <summary>
    /// EFCore切片供应商
    /// </summary>
    public sealed class EFCoreActionFilterProvider : IActionFilterProvider
    {
        // 私有变量
        private IDbContextWork? _dbContextAsyncWork;

        #region DI注入

        private readonly IDbContextWorkManager _dbContextAsyncManager;

        /// <summary>
        /// EFCore切片供应商
        /// </summary>
        public EFCoreActionFilterProvider(
            IDbContextWorkManager dbContextAsyncManager
            )
        {
            _dbContextAsyncManager = dbContextAsyncManager;
        }

        #endregion

        /// <summary>
        /// 执行开始
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            _dbContextAsyncWork = _dbContextAsyncManager.CreateWork();
        }

        /// <summary>
        /// 执行结束
        /// </summary>
        /// <param name="context"></param>
        public async void OnActionExecuted(ActionExecutedContext context)
        {
            if (_dbContextAsyncWork is null) return;
            await _dbContextAsyncWork.CompleteAsync();
            _dbContextAsyncWork.Dispose();
        }
    }
}
