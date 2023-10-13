using Suyaa.Data.Attributes;
using Suyaa.Data.Enums;
using Suyaa.Hosting.Kernel.Dependency;
using System.ComponentModel.DataAnnotations;

namespace Suyaa.Hosting.Entities
{

    /// <summary>
    /// 带主键的实例
    /// </summary>
    public abstract class Entity<TId> : IEntity<TId> where TId : notnull
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Key]
        [DbColumn(Convert = DbNameConvertTypes.UnderlineLower)]
        public virtual TId Id { get; set; }

        /// <summary>
        /// 带主键的实例
        /// </summary>
        /// <param name="id"></param>
        public Entity(TId id)
        {
            Id = id;
        }

    }
}
