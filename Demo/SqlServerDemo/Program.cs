// See https:/// See https://aka.ms/new-console-template for more information
using Suyaa;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using SqlServerDemo;

sy.Logger.Factory.UseStringAction(message =>
{
    Debug.WriteLine(message);
});

//string key = "ASPNETCORE_ENVIRONMENT";
//if (Environment.GetEnvironmentVariable(key).IsNullOrWhiteSpace()) Environment.SetEnvironmentVariable(key, "Production");
//var builder = new ConfigurationBuilder()
//               .SetBasePath(Directory.GetCurrentDirectory())
//               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
//               .AddEnvironmentVariables(prefix: "ASPNETCORE_")
//               .AddCommandLine(args);
//var config = builder.Build();

//sy.Hosting.CreateHost<Startup>(webBuilder => webBuilder.UseConfiguration(config), args).Run();
sy.Hosting.CreateWebApplication<DemoApplicationProvider>(args).Run();