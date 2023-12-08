using SimpleHosting.Entities;
using Suyaa.Data.Dependency;
using Suyaa.Hosting.App.Services;
using Suyaa.Hosting.Common.DependencyInjection.Dependency;
using Suyaa.Hosting.Common.DependencyInjection.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHosting.Datas
{
    /// <summary>
    /// 数据
    /// </summary>
    public sealed class DataApp : DomainServiceApp
    {
        private readonly IDependencyManager _dependencyManager;

        /// <summary>
        /// 数据
        /// </summary>
        /// <param name="dependencyManager"></param>
        public DataApp(
            IDependencyManager dependencyManager
            )
        {
            _dependencyManager = dependencyManager;
        }

        public async Task<Test> GetTest()
        {
            var testRepository = _dependencyManager.Resolve<IRepository<Test, string>>();
            var test = new Test();
            return await Task.FromResult(test);
        }
    }
}
