using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.DependencyInjection;
using Suyaa.Configure.Cores.Projects.Dto;
using Suyaa.Configure.Cores.Projects.Sto;
using Suyaa.Configure.Entity.Projects;
using Suyaa.Hosting.Data.Dependency;
using Suyaa.Hosting.Dependency;
using Suyaa.Hosting.Helpers;
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
    public class PorjectCore : IProjectCore
    {

        #region DI注入
        private readonly IRepository<Project, string> _projectRepository;
        private readonly IObjectMapper _objectMapper;
        private readonly IServiceProvider _provider;

        //private readonly IProjectCore _projectCore;

        /// <summary>
        /// 项目
        /// </summary>
        public PorjectCore(
            IRepository<Project, string> projectRepository,
            IObjectMapper objectMapper,
            IServiceProvider provider
            )
        {
            _projectRepository = projectRepository;
            _objectMapper = objectMapper;
            _provider = provider;
            //_projectCore = projectCore;
        }

        #endregion

        /// <summary>
        /// 获取 项目 查询
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public IQueryable<Project> GetQuery(Expression<Func<Project, bool>>? expression = null)
        {
            var _projectRepository = _provider.GetRequiredService<IRepository<Project, string>>();
            var query = from p in _projectRepository.Query()
                            .WhereIf(expression != null, expression)
                        orderby p.Name
                        select p;
            return query.AsNoTracking();
        }

        /// <summary>
        /// 获取 项目 列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedOutput<ProjectOutput>> GetList(ProjectListInput input)
        {
            var query = GetQuery();
            var projects = await query.ToListAsync();
            var output = new PagedOutput<ProjectOutput>(_objectMapper.Map<List<ProjectOutput>>(projects));
            return await Task.FromResult(output);
        }

        /// <summary>
        /// 新增 项目
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task Insert(Project input)
        {
            //await _projectRepository.InsertAsync(input);
        }

        /// <summary>
        /// 删除 项目
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task Delete(string id)
        {
            //await _projectRepository.DeleteAsync(id);
        }
    }
}
