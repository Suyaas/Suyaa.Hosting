using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Hosting.Common.DependencyInjection
{
    /// <summary>
    /// 依赖异常
    /// </summary>
    public class DependencyException : KeyException
    {
        /// <summary>
        /// 依赖
        /// </summary>
        public const string KEY_DEPENDENCY = "Dependency";

        /// <summary>
        /// 依赖异常
        /// </summary>
        /// <param name="type"></param>
        public DependencyException(Type type) : base(KEY_DEPENDENCY, "Type '{0}' dependency error.", type.FullName ?? string.Empty)
        {
            Type = type;
        }

        /// <summary>
        /// 依赖异常
        /// </summary>
        /// <param name="type"></param>
        /// <param name="message"></param>
        public DependencyException(Type type, string key, string message) : base(KEY_DEPENDENCY + "." + key, "Type '{0}' dependency error: " + message, type.FullName ?? string.Empty)
        {
            Type = type;
        }

        /// <summary>
        /// 类型
        /// </summary>
        public Type Type { get; }
    }
}
