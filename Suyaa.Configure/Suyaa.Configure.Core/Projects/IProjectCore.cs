using Suyaa.Configure.Cores.Projects.Dto;
using Suyaa.Configure.Cores.Projects.Sto;
using Suyaa.Configure.Entity.Projects;
using Suyaa.Hosting.Kernel.Dependency;
using Suyaa.Hosting.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Configure.Cores.Projects
{
    /// <summary>
    /// 项目
    /// </summary>
    public interface IProjectCore
    {
        /// <summary>
        /// 获取查询
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        IQueryable<Project> GetQuery(Expression<Func<Project, bool>> expression);

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedOutput<ProjectOutput>> GetList(ProjectListInput input);

        /// <summary>
        /// 新增 项目
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Insert(Project input);
    }
}
