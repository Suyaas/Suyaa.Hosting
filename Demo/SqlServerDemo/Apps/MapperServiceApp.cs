using Microsoft.AspNetCore.Mvc;
using SqlServerDemo.Cores.Dto;
using SqlServerDemo.Entities;
using Suyaa.Hosting.Kernel.Dependency;
using Suyaa.Hosting.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlServerDemo.Apps
{
    /// <summary>
    /// 对象映射测试
    /// </summary>
    public sealed class MapperServiceApp : ServiceApp
    {
        private readonly IObjectMapper _objectMapper;

        public MapperServiceApp(
            IObjectMapper objectMapper
            )
        {
            _objectMapper = objectMapper;
        }

        public async Task<SystemObjectsDto> GetMapperData(int count)
        {
            SystemObjects systemObjects = new SystemObjects()
            {
                Name = "ass",
                Version = "1.0.0",
            };
            var data = _objectMapper.Map<SystemObjectsDto>(systemObjects);
            return await Task.FromResult(data);
        }

        public async Task<SystemObjectsDto> GetDto(SystemObjects systemObjects)
        {
            var data = _objectMapper.Map<SystemObjectsDto>(systemObjects);
            return await Task.FromResult(data);
        }
    }
}
