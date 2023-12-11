// See https://aka.ms/new-console-template for more information
using Microsoft.AspNetCore.Builder;
using SimpleEfCoreHosting;
using Suyaa.Hosting.Infrastructure.WebApplications.Helpers;

WebApplication.CreateBuilder(args).Build<Startup>().Run();
