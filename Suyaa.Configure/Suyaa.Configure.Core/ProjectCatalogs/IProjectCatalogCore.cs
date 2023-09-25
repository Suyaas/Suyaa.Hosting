using Suyaa.Configure.Entity.ProjectCatalogs;
using Suyaa.Hosting.Kernel.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Configure.Cores.ProjectCatalogs
{
    /// <summary>
    /// 项目分类
    /// </summary>
    public interface IProjectCatalogCore : IServiceCore
    {
        /// <summary>
        /// 获取 项目 查询
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        IQueryable<ProjectCatalog> GetQuery(Expression<Func<ProjectCatalog, bool>>? expression = null);

        /// <summary>
        /// 修改 项目
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Insert(ProjectCatalog input);
    }
}
