using Suyaa.Data.Attributes;
using Suyaa.Data.Enums;

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
