using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Suyaa.Configure.Basic.Dependency;
using Suyaa.Configure.Basic.Infos;
using Suyaa.Configure.Cores.ProjectCatalogs;
using Suyaa.Configure.Cores.ProjectCatalogs.Dto;
using Suyaa.Configure.Cores.ProjectCatalogs.Sto;
using Suyaa.Configure.Cores.Projects;
using Suyaa.Configure.Cores.Projects.Dto;
using Suyaa.Configure.Cores.Projects.Sto;
using Suyaa.Configure.Entity.ProjectCatalogs;
using Suyaa.Configure.Entity.Projects;
using Suyaa.Hosting.Attributes;
using Suyaa.Hosting.Dependency;
using Suyaa.Hosting.Pages;
using Suyaa.Hosting.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Configure.Apps.ProjectCatalogs
{
    /// <summary>
    /// 项目分类
    /// </summary>
    //[App("CatalogManage")]
    [JwtAuthorize]
    public class CatalogManageApp : ServiceApp
    {
        #region DI注入
        private readonly IProjectCatalogCore _projectCatalogCore;
        private readonly IMapper _mapper;

        /// <summary>
        /// 项目
        /// </summary>
        public CatalogManageApp(
            IProjectCatalogCore projectCatalogCore,
            IMapper mapper
            )
        {
            _projectCatalogCore = projectCatalogCore;
            _mapper = mapper;
        }

        #endregion

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Put]
        public async Task<PagedOutput<ProjectCatalogOutput>> GetList(ProjectCatalogListInput input)
        {
            var query = _projectCatalogCore.GetQuery();
            var count = await query.CountAsync();
            var datas = await query.ToListAsync();
            var outputs = _mapper.Map<List<ProjectCatalogOutput>>(datas);
            return new PagedOutput<ProjectCatalogOutput>(outputs)
            {
                RowCount = count
            };
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Put]
        public async Task Insert(ProjectCatalogInsertInput input)
        {
            await _projectCatalogCore.Insert(_mapper.Map<ProjectCatalog>(input));
        }
    }
}
