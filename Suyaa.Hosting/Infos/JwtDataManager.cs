using Suyaa.Hosting.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Hosting.Infos
{
    /// <summary>
    /// Jwt信息
    /// </summary>
    public class JwtDataManager : IJwtDataManager
    {
        /// <summary>
        /// 数据
        /// </summary>
        public IJwtData? Data { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public long UserId => this.Data?.UserId ?? 0;
    }

    /// <summary>
    /// Jwt信息
    /// </summary>
    public class JwtData : IJwtData
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public long UserId { get; set; } = 0;
    }

    /// <summary>
    /// Jwt类型信息
    /// </summary>
    public class JwtDataType : IJwtDataType
    {
        /// <summary>
        /// 类型
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// Jwt类型信息
        /// </summary>
        /// <param name="type"></param>
        public JwtDataType(Type type)
        {
            Type = type;
        }
    }
}
