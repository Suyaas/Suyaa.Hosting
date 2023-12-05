using Suyaa.Hosting.Common.DependencyManager.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Hosting.Common.DependencyManager
{
    /// <summary>
    /// 类型集合
    /// </summary>
    internal static class Types
    {
        /// <summary>
        /// 独占
        /// </summary>
        public static Type Exclusive => typeof(IDependencyExclusive);

        /// <summary>
        /// 单例
        /// </summary>
        public static Type Singleton => typeof(IDependencySingleton);

        /// <summary>
        /// 瞬态
        /// </summary>
        public static Type Transient => typeof(IDependencyTransient);
    }
}
