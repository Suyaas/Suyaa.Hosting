using Microsoft.EntityFrameworkCore;
using SqlServerDemo.Entities;
using Suyaa.Hosting.Data.Dependency;
using Suyaa.Hosting.Services;
using Suyaa.Hosting;
using Suyaa.Hosting.Kernel;

namespace SqlServerDemo.Apps
{
    /// <summary>
    /// SqlServer
    /// </summary>
    public class SqlServerServiceApp : ServiceApp
    {
        private readonly IRepository<SystemObjects, decimal> _systemObjectsRepository;
        private readonly IRepository<SystemTables, decimal> _systemTablesRepository;

        public SqlServerServiceApp(
            IRepository<SystemObjects, decimal> systemObjectsRepository,
            IRepository<SystemTables, decimal> systemTablesRepository
            )
        {
            _systemObjectsRepository = systemObjectsRepository;
            _systemTablesRepository = systemTablesRepository;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <returns></returns>
        public async Task Insert()
        {
            var systemObjects = await _systemObjectsRepository.Query()
                .Where(d => d.Name == "SqlServerServiceApp")
                .FirstOrDefaultAsync();
            if (systemObjects != null) throw new UserFriendlyException($"已存在'SqlServerServiceApp'");
            systemObjects = new SystemObjects()
            {
                Name = "SqlServerServiceApp",
                Version = "1.0"
            };
            await _systemObjectsRepository.InsertAsync(systemObjects);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        public async Task Update()
        {
            var systemObjects = await _systemObjectsRepository.Query()
                .Where(d => d.Name == "SqlServerServiceApp")
                .FirstOrDefaultAsync();
            if (systemObjects is null) throw new UserFriendlyException($"未找到'SqlServerServiceApp'");
            systemObjects.Version = "1.1";
            await _systemObjectsRepository.UpdateAsync(systemObjects);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        public async Task Delete()
        {
            var systemObjects = await _systemObjectsRepository.Query()
                .Where(d => d.Name == "SqlServerServiceApp")
                .FirstOrDefaultAsync();
            if (systemObjects is null) throw new UserFriendlyException($"未找到'SqlServerServiceApp'");
            await _systemObjectsRepository.DeleteAsync(systemObjects);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public async Task<List<SystemTables>> GetList()
        {
            var query = from so in _systemObjectsRepository.Query()
                        join st in _systemTablesRepository.Query() on so.Id equals st.ObjectID
                        orderby so.Id descending
                        select st;
            var list = await query.AsNoTracking().Take(10).ToListAsync();
            return list;
        }
    }
}
