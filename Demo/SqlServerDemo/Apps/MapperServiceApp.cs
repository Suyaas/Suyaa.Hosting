using Microsoft.AspNetCore.Mvc;
using SqlServerDemo.Cores.Dto;
using SqlServerDemo.Entities;
using Suyaa.Hosting.Kernel.Dependency;
using Suyaa.Hosting.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public async Task<SystemObjectsDto> GetMapperDataByName([Required] string name)
        {
            SystemObjects systemObjects = new SystemObjects()
            {
                Name = name,
                Version = "1.0.0",
            };
            var data = _objectMapper.Map<SystemObjectsDto>(systemObjects);
            return await Task.FromResult(data);
        }

        public async Task<SystemObjectsDto> GetMapperData(MapperTestInput input)
        {
            SystemObjects systemObjects = new SystemObjects()
            {
                Name = input.Name,
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
