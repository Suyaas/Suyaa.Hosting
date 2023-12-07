using Suyaa.Hosting.App.Services;
using Suyaa.Hosting.Common.Configures;
using Suyaa.Hosting.Common.Configures.Dependency;
using Suyaa.Hosting.Kernels.Dto;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Hosting.Modules
{
    /// <summary>
    /// 模块
    /// </summary>
    public sealed class ModuleApp : DomainServiceApp
    {

        /// <summary>
        /// 获取模块信息
        /// </summary>
        /// <returns></returns>
        public async Task<ModuleInfoOutput> GetInfo()
        {
            var type = typeof(ModuleStartup);
            var assembly = type.Assembly;
            var filePath = assembly.Location;
            var fileInfo = FileVersionInfo.GetVersionInfo(filePath);
            var output = new ModuleInfoOutput()
            {
                Name = fileInfo.ProductName ?? string.Empty,
                Version = fileInfo.ProductVersion ?? string.Empty,
            };
            return await Task.FromResult(output);
        }

    }
}
