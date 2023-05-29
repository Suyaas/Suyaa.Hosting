using AutoMapper;
using Suyaa.Configure.Basic.Dependency;
using Suyaa.Configure.Cores.Projects;
using Suyaa.Configure.Cores.Projects.Dto;
using Suyaa.Configure.Cores.Projects.Sto;
using Suyaa.Configure.Entity.Projects;
using Suyaa.Hosting.Dependency;
using Suyaa.Hosting.Dependency.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Configure.Apps.Projects
{
    /// <summary>
    /// 项目
    /// </summary>
    [App("Project")]
    [AppAuthorize]
    public class ProjectApp : ServiceApp
    {
        #region DI注入
        private readonly IProjectCore _projectCore;
        private readonly IMapper _mapper;

        /// <summary>
        /// 项目
        /// </summary>
        public ProjectApp(
            IProjectCore projectCore,
            IMapper mapper
            )
        {
            _projectCore = projectCore;
            _mapper = mapper;
        }

        #endregion

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Put]
        public async Task<PagedOutput<ProjectOutput>> GetList(ProjectListInput input)
        {
            return await _projectCore.GetList(input);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Put]
        public async Task Insert(ProjectInsertInput input)
        {
            await _projectCore.Insert(_mapper.Map<Project>(input));
        }
    }
}
