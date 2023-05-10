using Suyaa.Data;
using Suyaa.Data.Entities;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Apis.Dependency
{
    /// <summary>
    /// 数据仓库
    /// </summary>
    public class Repository<TClass, Tid> : Suyaa.Data.Repository<TClass, Tid>
        where TClass : class, IEntity<Tid>
    {
        // 连接集合
        private readonly IDatabaseConnection _connection;

        /// <summary>
        /// 数据仓库
        /// </summary>
        public Repository(IDatabaseConnection connection) : base(connection)
        {
            _connection = connection;
        }
    }
}
