using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Hosting.EFCore.Dependency
{
    /// <summary>
    /// 数据库上下文异步管理器
    /// </summary>
    public interface IDbContextAsyncProvider
    {
        /// <summary>
        /// 获取一个异步工作
        /// </summary>
        /// <returns></returns>
        IDbContextAsyncWork? GetCurrentWork();

        /// <summary>
        /// 设置一个异步工作
        /// </summary>
        /// <returns></returns>
        void SetCurrentWork(IDbContextAsyncWork? work);
    }
}
