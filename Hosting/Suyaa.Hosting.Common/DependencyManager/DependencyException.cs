using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Hosting.Common.DependencyManager
{
    /// <summary>
    /// 依赖异常
    /// </summary>
    public class DependencyException : Exception
    {
        /// <summary>
        /// 依赖异常
        /// </summary>
        /// <param name="type"></param>
        public DependencyException(Type type) : base($"Type '{type.FullName}' dependency error.")
        {
            Type = type;
        }

        /// <summary>
        /// 依赖异常
        /// </summary>
        /// <param name="type"></param>
        /// <param name="message"></param>
        public DependencyException(Type type, string message) : base($"Type '{type.FullName}' dependency error: {message}")
        {
            Type = type;
        }

        /// <summary>
        /// 类型
        /// </summary>
        public Type Type { get; }
    }
}
