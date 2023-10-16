using AutoMapper;
using Suyaa.DependencyInjection;
using Suyaa.Hosting.Kernel.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Hosting.AutoMapper
{
    /// <summary>
    /// 对象映射器
    /// </summary>
    public class ObjectMapper : IObjectMapper, IDependencyTransient
    {
        #region DI注入

        private readonly IMapper _mapper;

        public ObjectMapper(
            IMapper mapper
            )
        {
            _mapper = mapper;
        }

        #endregion

        /// <summary>
        /// 映射
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public T Map<T>(object data)
        {
            return _mapper.Map<T>(data);
        }

    }
}
