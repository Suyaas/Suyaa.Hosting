using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Suyaa.Hosting.EFCore.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Hosting.EFCore
{
    /// <summary>
    /// 设计时数据上下文
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class DesignTimeDbContextFactoryBase<T> : IDesignTimeDbContextFactory<T>
        where T : DbContext
    {
        /// <summary>
        /// 创建DbContext实例
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public abstract T CreateDbContext(IDbConnectionDescriptorFactory dbConnectionDescriptorFactory);

        /// <summary>
        /// 创建配置
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public abstract IConfigurationRoot CreateConfiguration(string[] args);

        /// <summary>
        /// 创建DbContext实例
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public T CreateDbContext(string[] args)
        {
            var configuration = this.CreateConfiguration(args);
            var dbConnectionDescriptorFactory = new DbConnectionDescriptorFactory(configuration);
            return this.CreateDbContext(dbConnectionDescriptorFactory);
        }
    }
}
