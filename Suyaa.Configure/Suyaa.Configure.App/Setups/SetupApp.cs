using Suyaa.Configure.Entity.Projects;
using Suyaa.Data;
using Suyaa.Hosting.Dependency;

namespace Suyaa.Configure.App.Setups
{
    /// <summary>
    /// 安装
    /// </summary>
    [App("Setup")]
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
        public async Task Init()
        {
            var connection = (DatabaseConnection)_connection;
            connection.Open();
            // 创建项目表
            await connection.TableCreated<Project>();
        }
    }
}