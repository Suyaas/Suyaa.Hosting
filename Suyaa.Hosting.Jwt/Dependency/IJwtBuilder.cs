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
        /// <param name="type"></param>
        /// <returns></returns>
        TData GetData(string token);

        /// <summary>
        /// 创建一个Jwt令牌
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        JwtToken CreateToken(TData data, DateTime? expires = null);
    }
}
