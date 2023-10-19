using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Hosting.Jwt.Dependency
{
    /// <summary>
    /// Jwt数据供应商
    /// </summary>
    public interface IJwtDataProvider<TData>
        where TData : class, IJwtData
    {
        /// <summary>
        /// 创建一个Jwt数据
        /// </summary>
        /// <returns></returns>
        TData CreateJwtData();

        /// <summary>
        /// 构建器
        /// </summary>
        /// <returns></returns>
        IJwtBuilder<TData> Builder { get; }
    }
}
