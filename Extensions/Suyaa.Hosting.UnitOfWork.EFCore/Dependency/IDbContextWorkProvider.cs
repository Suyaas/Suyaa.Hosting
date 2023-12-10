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
    public interface IDbContextWorkProvider
    {
        /// <summary>
        /// 创建一个异步工作
        /// </summary>
        /// <returns></returns>
        IDbContextWork CreateWork();
    }
}
