using Suyaa.Configure.Basic.Dependency;
using Suyaa.Configure.Cores.Projects;
using Suyaa.Configure.Cores.Projects.Dto;
using Suyaa.Configure.Cores.Projects.Sto;
using Suyaa.Hosting.Dependency;
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

        /// <summary>
        /// 项目
        /// </summary>
        public ProjectApp(
            IProjectCore projectCore
            )
        {
            _projectCore = projectCore;
        }

        #endregion

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedOutput<ProjectOutput>> GetList(ProjectListInput input)
        {
            return await _projectCore.GetList(input);
        }
    }
}
