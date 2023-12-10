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
    public interface IDbContextWorkManager
    {
        /// <summary>
        /// 创建一个作业
        /// </summary>
        /// <returns></returns>
        IDbContextWork CreateWork();

        /// <summary>
        /// 获取当前作业
        /// </summary>
        /// <returns></returns>
        IDbContextWork? GetWork();

        /// <summary>
        /// 释放作业
        /// </summary>
        /// <returns></returns>
        void ReleaseWork();
    }
}
