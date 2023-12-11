using SimpleEfCoreHosting.Entities;
using Suyaa.Data.Dependency;
using Suyaa.EFCore.Dependency;
using Suyaa.Hosting.App.Services;
using Suyaa.Hosting.Common.Configures;
using Suyaa.Hosting.Common.Configures.Dependency;
using Suyaa.Hosting.Common.DependencyInjection.Dependency;
using Suyaa.Hosting.Common.DependencyInjection.Helpers;
using Suyaa.Hosting.Jwt.Configures;
using Suyaa.Logs.Dependency;

namespace SimpleEfCoreHosting.Datas
{
    /// <summary>
    /// 数据
    /// </summary>
    public sealed class DataApp : DomainServiceApp
    {
        private readonly IDependencyManager _dependencyManager;
        private readonly ILogger _logger;

        /// <summary>
        /// 数据
        /// </summary>
        /// <param name="dependencyManager"></param>
        public DataApp(
            IDependencyManager dependencyManager,
            ILogger logger
            )
        {
            _dependencyManager = dependencyManager;
            _logger = logger;
        }

        public async Task<string> GetJwtConfig()
        {
            var jwtConfig = _dependencyManager.ResolveRequired<IOptionConfig<JwtConfig>>();
            return await Task.FromResult(jwtConfig.CurrentValue.TokenKey);
        }

        public async Task<Test> GetTest()
        {
            var dbFactory = _dependencyManager.ResolveRequired<IDbFactory>();
            var dbContextFactory = _dependencyManager.ResolveRequired<IDbContextFactory>();
            var workNew = _dependencyManager.ResolveRequired<IDbWork>();
            var test = new Test();
            var testRepository = _dependencyManager.ResolveRequired<IRepository<Test, string>>();
            testRepository.Insert(test);
            //try
            //{

            //}
            //catch (Exception ex)
            //{
            //    System.Reflection.Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            //    foreach (var assembly in assemblies)
            //    {
            //        if (assembly.ManifestModule.Name != "Suyaa.Data.PostgreSQL.dll") continue;
            //        try
            //        {
            //            var types = assembly.GetTypes();
            //            foreach (var type in types)
            //            {
            //                if (type.FullName == "Suyaa.Data.PostgreSQL.Providers.PostgreSqlProvider")
            //                {
            //                    var obj = sy.Assembly.Create(type);
            //                }
            //            }
            //        }
            //        catch (Exception ex1)
            //        {
            //            _logger.Error(ex1.ToString());
            //        }
            //    }
            //    //var obj = sy.Assembly.Create("Suyaa.Data.PostgreSQL.Providers.PostgreSqlProvider");
            //    _logger.Error(ex.ToString());
            //}
            return await Task.FromResult(test);
        }
    }
}
