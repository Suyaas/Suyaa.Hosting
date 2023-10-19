using Suyaa.Configure.Basic.Jwt;
using Suyaa.Configure.Entity.ProjectCatalogs;
using Suyaa.Configure.Entity.Projects;
using Suyaa.Data;
using Suyaa.Hosting.Kernel.Attributes;
using Suyaa.Hosting.Services;

namespace Suyaa.Configure.App.Setups
{
    /// <summary>
    /// 安装
    /// </summary>
    [App("Setup")]
    [JwtAuthorize]
    public class SetupApp : ServiceApp
    {
        private readonly IDatabaseConnection _connection;

        /// <summary>
        /// 安装
        /// </summary>
        public SetupApp(
            IDatabaseConnection connection
            )
        {
            _connection = connection;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        [Get]
        public async Task Init()
        {
            var connection = (DatabaseConnection)_connection;
            connection.Open();
            // 创建项目表
            await connection.TableCreated<ProjectCatalog>();
            await connection.TableCreated<Project>();
        }
    }
}