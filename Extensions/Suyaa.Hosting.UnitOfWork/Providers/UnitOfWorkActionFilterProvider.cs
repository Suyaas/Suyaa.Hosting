using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Suyaa.Hosting.Common.Exceptions;
using Suyaa.Hosting.Infrastructure.Exceptions.Enums;
using Suyaa.Hosting.Common.ActionFilters.Dependency;
using Suyaa.Hosting.UnitOfWork.Dependency;

namespace Suyaa.Hosting.UnitOfWork.Providers
{
    /// <summary>
    /// EFCore切片供应商
    /// </summary>
    public sealed class UnitOfWorkActionFilterProvider : IActionFilterProvider
    {

        #region 依赖注入

        private readonly IUnitOfWorkManager _unitOfWorkManager;

        /// <summary>
        /// EFCore切片供应商
        /// </summary>
        public UnitOfWorkActionFilterProvider(
            IUnitOfWorkManager unitOfWorkManager
            )
        {
            _unitOfWorkManager = unitOfWorkManager;
        }

        #endregion

        /// <summary>
        /// 执行开始
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            _unitOfWorkManager.Begin();
        }

        /// <summary>
        /// 执行结束
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            var work = _unitOfWorkManager.GetWork();
            if (work is null) return;
            work.Complete();
            _unitOfWorkManager.ReleaseWork();
        }
    }
}
