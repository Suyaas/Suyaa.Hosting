using Microsoft.AspNetCore.Builder;
using Suyaa.Hosting.Infrastructure.WebApplications.Helpers;
using Suyaa.Hosting.WebApplications;

WebApplication.CreateBuilder(args).Build<HostStartup>().Run();
