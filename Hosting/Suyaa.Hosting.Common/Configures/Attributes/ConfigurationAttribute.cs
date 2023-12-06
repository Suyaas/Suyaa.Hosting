using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Hosting.Common.Configures.Attributes
{
    /// <summary>
    /// 配置
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public sealed class ConfigurationAttribute : Attribute
    {
        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="name"></param>
        public ConfigurationAttribute(string name)
        {
            Name = name;
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; }
    }
}
