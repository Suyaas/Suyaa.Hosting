using Microsoft.AspNetCore.Builder;
using Suyaa.Hosting.Infrastructure.WebApplications;
using Suyaa.Hosting.Infrastructure.WebApplications.Helpers;

WebApplication.CreateBuilder(args).Build<BaseStartup>().Run();