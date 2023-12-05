using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Hosting.Jwt.Dependency
{
    /// <summary>
    /// Jwt管理器
    /// </summary>
    public interface IJwtManager<TData>
        where TData : class, IJwtData
    {

        /// <summary>
        /// 获取数据
        /// </summary>
        TData GetCurrentData();

        /// <summary>
        /// 设置数据
        /// </summary>
        void SetCurrentData(TData data);

        /// <summary>
        /// 数据供应商
        /// </summary>
        IJwtDataProvider<TData> Provider { get; }
    }
}
