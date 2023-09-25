using Suyaa.Data.Dependency;
using Suyaa.Data.Dependency.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Hosting.Entities
{
    /// <summary>
    /// 带雪花Id主键的实例
    /// </summary>
    public class SnowflakeKeyEntity : Entity<long>
    {

        /// <summary>
        /// 自动增长标识
        /// </summary>
        [DbColumnType(DbColumnTypes.BigInt)]
        public override long Id { get => base.Id; set => base.Id = value; }

        /// <summary>
        /// 对象实例化
        /// </summary>
        public SnowflakeKeyEntity() : base(sy.Generator.GetSnowflakeId()) { }

    }
}
