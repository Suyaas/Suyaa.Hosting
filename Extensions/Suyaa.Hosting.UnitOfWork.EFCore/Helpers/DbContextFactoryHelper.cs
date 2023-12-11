using Suyaa.EFCore.Dependency;
using Suyaa.Hosting.EFCore.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Hosting.UnitOfWork.EFCore.Helpers
{
    /// <summary>
    /// 数据库上下文工厂助手
    /// </summary>
    public static class DbContextFactoryHelper
    {
        ///// <summary>
        ///// 获取所有的实例类型
        ///// </summary>
        ///// <param name="dbContextFactory"></param>
        ///// <param name="dbConnectionDescriptorName"></param>
        ///// <returns></returns>
        //public static IList<Type> GetEntityTypes(this IDbContextFactory dbContextFactory, string dbConnectionDescriptorName)
        //{
        //    var entities = dbContextFactory.GetEntities(dbConnectionDescriptorName);
        //    var types = new List<Type>();
        //    foreach (var entity in entities)
        //    {
        //        types.Add(entity.Entity);
        //    }
        //    return types;
        //}
    }
}
