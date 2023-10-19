using AutoMapper;
using Suyaa.Configure.Basic.Dependency;
using Suyaa.Configure.Basic.Jwt;
using Suyaa.Configure.Cores.Projects;
using Suyaa.Configure.Cores.Projects.Dto;
using Suyaa.Configure.Cores.Projects.Sto;
using Suyaa.Configure.Entity.Projects;
using Suyaa.Hosting.Jwt.Dependency;
using Suyaa.Hosting.Kernel.Attributes;
using Suyaa.Hosting.Pages;
using Suyaa.Hosting.Services;

namespace Suyaa.Configure.Apps.Projects
{
    /// <summary>
    /// 项目
    /// </summary>
    [App("ProjectManage")]
    [JwtAuthorize]
    public class ProjectManageApp : ServiceApp
    {
        #region DI注入
        private readonly IProjectCore _projectCore;
        private readonly IMapper _mapper;
        private readonly IJwtManager _jwtManager;

        //private readonly IJwtData _jwtData;

        /// <summary>
        /// 项目
        /// </summary>
        public ProjectManageApp(
            IProjectCore projectCore,
            IMapper mapper,
            IJwtManager jwtManager
            //IJwtData jwtData
            )
        {
            _projectCore = projectCore;
            _mapper = mapper;
            _jwtManager = jwtManager;
            //_jwtData = jwtData;
        }

        #endregion

        /// <summary>
        /// 获取Jwt信息
        /// </summary>
        /// <returns></returns>
        [Get]
        public async Task<JwtInfo> GetJwtInfo()
        {
            return await Task.FromResult((JwtInfo)_jwtManager.GetCurrentData()!);
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
