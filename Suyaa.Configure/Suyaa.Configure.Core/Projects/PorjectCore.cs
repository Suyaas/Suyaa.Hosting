using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Suyaa.Configure.Cores.Projects.Dto;
using Suyaa.Configure.Cores.Projects.Sto;
using Suyaa.Configure.Entity.Projects;
using Suyaa.EFCore.Dbsets;
using Suyaa.Hosting.Dependency;
using Suyaa.Hosting.Helpers;
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
    public class PorjectCore : IProjectCore
    {

        #region DI注入
        private readonly IEfRepository<Project, string> _projectRepository;
        private readonly IProjectCore _projectCore;

        /// <summary>
        /// 项目
        /// </summary>
        public PorjectCore(
            IEfRepository<Project, string> projectRepository,
            IProjectCore projectCore
            )
        {
            _projectRepository = projectRepository;
            _projectCore = projectCore;
        }

        #endregion

        /// <summary>
        /// 获取查询
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public IQueryable<Project> GetQuery(Expression<Func<Project, bool>>? expression = null)
        {
            var query = from p in _projectRepository.Query()
                            .WhereIf(expression != null, expression)
                        orderby p.Name
                        select p;
            return query.AsNoTracking();
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedOutput<ProjectOutput>> GetList(ProjectListInput input)
        {
            var query = GetQuery();
            var result = await query.ToListAsync();
            return new PagedOutput<ProjectOutput>();
        }
    }
}
