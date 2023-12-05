using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Hosting.Jwt.Dependency
{
    /// <summary>
    /// Jwt构建器
    /// </summary>
    public interface IJwtBuilder<TData>
        where TData : class, IJwtData
    {
        /// <summary>
        /// 转化为Jwt对象
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        TData GetData(string token);

        /// <summary>
        /// 创建一个Jwt令牌
        /// </summary>
        /// <param name="data"></param>
        /// <param name="expires"></param>
        JwtToken CreateToken(TData data, DateTime? expires = null);
    }
}
