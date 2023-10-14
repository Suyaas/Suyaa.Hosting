// See https://aka.ms/new-console-template for more information
using Suyaa;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using Suyaa.Configure.Host;

sy.Logger.GetCurrentLogger()
    .Use((string message) =>
    {
        Debug.WriteLine(message);
    });

string key = "ASPNETCORE_ENVIRONMENT";
string env = Environment.GetEnvironmentVariable(key) ?? "Production";
if (Environment.GetEnvironmentVariable(key).IsNullOrWhiteSpace()) Environment.SetEnvironmentVariable(key, env);
var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .AddJsonFile($"appsettings.{env}.json", optional: false, reloadOnChange: true)
               .AddEnvironmentVariables(prefix: "ASPNETCORE_")
               .AddCommandLine(args);
var config = builder.Build();

// 启动器
//Startup startup = new Startup(config);

sy.Hosting.CreateHost<Startup>(webBuilder => webBuilder.UseConfiguration(config), args).Run();
