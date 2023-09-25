using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Hosting.Kernel.Dependency
{
    /// <summary>
    /// Jwt管理器
    /// </summary>
    public interface IJwtManager
    {
        /// <summary>
        /// 获取当前数据
        /// </summary>
        IJwtData? Current { get; }

        /// <summary>
        /// 数据
        /// </summary>
        IJwtData? GetCurrentData();

        /// <summary>
        /// 数据
        /// </summary>
        void SetCurrentData(IJwtData jwtData);
    }
}
