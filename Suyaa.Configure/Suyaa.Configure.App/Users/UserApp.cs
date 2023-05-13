using Suyaa.Configure.Entity.Projects;
using Suyaa.Data;
using Suyaa.Hosting.Dependency;

namespace Suyaa.Configure.App.Users
{
    /// <summary>
    /// 用户
    /// </summary>
    [App("User")]
    public class UserApp : ServiceApp
    {
        private readonly IDatabaseConnection _connection;

        /// <summary>
        /// 用户
        /// </summary>
        public UserApp(
            IDatabaseConnection connection
            )
        {
            _connection = connection;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        [Put]
        public async Task Login()
        {
            var connection = (DatabaseConnection)_connection;
            connection.Open();
            // 创建项目表
            await connection.TableCreated<Project>();
        }
    }
}