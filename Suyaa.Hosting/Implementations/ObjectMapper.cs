using Suyaa.DependencyInjection;
using Suyaa.Hosting.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Hosting.Implementations
{
    /// <summary>
    /// 对象映射器
    /// </summary>
    public class ObjectMapper : IObjectMapper, IDependencyTransient
    {
        /// <summary>
        /// 对象映射
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public T Map<T>(object data)
        {
            return (T)data;
        }
    }
}
