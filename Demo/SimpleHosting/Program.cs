using Microsoft.AspNetCore.Builder;
using SimpleHosting;
using Suyaa.Hosting.Infrastructure.WebApplications.Helpers;

WebApplication.CreateBuilder(args).Build<Startup>().Run();
