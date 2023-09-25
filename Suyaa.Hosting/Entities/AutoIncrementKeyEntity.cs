using Suyaa.Data.Dependency;
using Suyaa.Data.Dependency.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Hosting.Entities
{
    /// <summary>
    /// 带自增长主键的实例
    /// </summary>
    public class AutoIncrementKeyEntity : Entity<long>
    {

        /// <summary>
        /// 自动增长标识
        /// </summary>
        [DbAutoIncrement]
        [DbColumnType(DbColumnTypes.BigInt)]
        public override long Id { get => base.Id; set => base.Id = value; }

        /// <summary>
        /// 带自增长主键的实例
        /// </summary>
        public AutoIncrementKeyEntity() : base(0) { }

    }
}
