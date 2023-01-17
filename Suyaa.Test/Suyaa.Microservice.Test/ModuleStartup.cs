﻿using Microsoft.Extensions.DependencyInjection;
using Suyaa.Microservice.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Microservice.Test
{
    public class ModuleStartup : IModuleStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //throw new NotImplementedException();
            services.AddModulerIoc(Assembly.GetExecutingAssembly());
        }
    }
}
