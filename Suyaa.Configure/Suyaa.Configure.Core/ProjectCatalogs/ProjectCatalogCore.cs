using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Suyaa.Configure.Cores.Projects.Dto;
using Suyaa.Configure.Cores.Projects.Sto;
using Suyaa.Configure.Entity.ProjectCatalogs;
using Suyaa.Configure.Entity.Projects;
using Suyaa.Hosting.Data.Dependency;
using Suyaa.Hosting.Helpers;
using Suyaa.Hosting.Kernel.Dependency;
using Suyaa.Hosting.Pages;
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
    public class ProjectCatalogCore : IProjectCatalogCore
    {
        #region DI注入
        private readonly IRepository<ProjectCatalog, string> _projectCatalogRepository;
        private readonly IObjectMapper _objectMapper;
        //private readonly IServiceProvider _provider;

        //private readonly IProjectCore _projectCore;

        /// <summary>
        /// 关联数据仓库
        /// </summary>
        public IRepository<ProjectCatalog, string> Repository => _projectCatalogRepository;

        /// <summary>
        /// 项目分类
        /// </summary>
        public ProjectCatalogCore(
            IRepository<ProjectCatalog, string> projectCatalogRepository,
            IObjectMapper objectMapper
            //IServiceProvider provider
            )
        {
            _projectCatalogRepository = projectCatalogRepository;
            _objectMapper = objectMapper;
            //_provider = provider;
            //_projectCore = projectCore;
        }

        #endregion

        /// <summary>
        /// 获取 项目 查询
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public IQueryable<ProjectCatalog> GetQuery(Expression<Func<ProjectCatalog, bool>>? expression = null)
        {
            //var _projectRepository = _provider.GetRequiredService<IRepository<Project, string>>();
            var query = from p in _projectCatalogRepository.Query()
                            .WhereIf(expression != null, expression)
                        orderby p.Name
                        select p;
            return query.AsNoTracking();
        }

        /// <summary>
        /// 修改 项目
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task Update(ProjectCatalog input)
        {
            await _projectCatalogRepository.UpdateAsync(input);
        }

        /// <summary>
        /// 修改 项目
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task Insert(ProjectCatalog input)
        {
            await _projectCatalogRepository.InsertAsync(input);
        }

        /// <summary>
        /// 删除 项目
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task Delete(string id)
        {
            //await _projectCatalogRepository.DeleteAsync(id);
            await Task.CompletedTask;
        }
    }
}
