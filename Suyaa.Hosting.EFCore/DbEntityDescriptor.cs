using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Hosting.EFCore
{
    /// <summary>
    /// 数据库实例描述器
    /// </summary>
    public class DbEntityDescriptor
    {
        /// <summary>
        /// 实例类型
        /// </summary>
        public Type Entity { get; }

        /// <summary>
        /// 实例类型
        /// </summary>
        public Type Context { get; }

        /// <summary>
        /// 实例类型
        /// </summary>
        public PropertyInfo DbSet { get; }

        /// <summary>
        /// 数据库实例描述器
        /// </summary>
        public DbEntityDescriptor(Type context, PropertyInfo dbSet)
        {
            this.Entity = dbSet.PropertyType.GenericTypeArguments[0];
            this.Context = context;
            this.DbSet = dbSet;
        }
    }
}
