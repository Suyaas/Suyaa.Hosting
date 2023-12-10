using Suyaa.Data.Dependency;
using Suyaa.Data.Descriptors;
using Suyaa.EFCore.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Hosting.UnitOfWork.EFCore.Descriptors
{
    /// <summary>
    /// DbSet 描述
    /// </summary>
    public class DbSetDescriptor : Suyaa.EFCore.Descriptors.DbSetDescriptor
    {
        /// <summary>
        /// DbSet 描述
        /// </summary>
        /// <param name="type"></param>
        /// <param name="property"></param>
        /// <param name="dbContext"></param>
        public DbSetDescriptor(Type type, PropertyInfo property, IDescriptorDbContext dbContext) : base(type, property, dbContext.ConnectionDescriptor)
        {
            DbContext = dbContext.GetType();
        }

        /// <summary>
        /// 数据上下文类型
        /// </summary>
        public Type DbContext { get; }

    }
}
