using AutoMapper;
using Suyaa.Configure.Basic.AppAuthorize.Attributes;
using Suyaa.Configure.Basic.AppAuthorize.Dependency;
using Suyaa.Configure.Cores.Projects;
using Suyaa.Configure.Cores.Projects.Dto;
using Suyaa.Configure.Cores.Projects.Sto;
using Suyaa.Configure.Entity.Projects;
using Suyaa.Hosting.Kernel.Attributes;
using Suyaa.Hosting.Pages;
using Suyaa.Hosting.Services;

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
        private readonly IAppInfo _appInfo;

        /// <summary>
        /// 项目
        /// </summary>
        public ProjectApp(
            IProjectCore projectCore,
            IMapper mapper,
            IAppInfo appInfo
            )
        {
            _projectCore = projectCore;
            _mapper = mapper;
            _appInfo = appInfo;
        }

        #endregion

        /// <summary>
        /// 获取应用测试数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<string> AppTest(ProjectListInput input)
        {
            return await Task.FromResult(_appInfo.AppId);
        }

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
