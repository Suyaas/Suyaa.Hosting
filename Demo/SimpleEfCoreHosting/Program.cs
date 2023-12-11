// See https://aka.ms/new-console-template for more information
using Microsoft.AspNetCore.Builder;
using SimpleEfCoreHosting;
using Suyaa.Hosting.Infrastructure.WebApplications.Helpers;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true); 
AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
WebApplication.CreateBuilder(args).Build<Startup>().Run();
