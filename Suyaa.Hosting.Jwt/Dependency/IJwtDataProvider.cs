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
    public interface IJwtDataProvider
    {
        /// <summary>
        /// 创建一个Jwt数据
        /// </summary>
        /// <returns></returns>
        IJwtData CreateJwtData();
    }
}
